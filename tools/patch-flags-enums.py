#!/usr/bin/env python3
# This file is part of Project Hybrasyl.
#
# This program is free software; you can redistribute it and/or modify
# it under the terms of the Affero General Public License as published by
# the Free Software Foundation, version 3.
#
# This program is distributed in the hope that it will be useful, but
# without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
# or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
# for more details.
#
# You should have received a copy of the Affero General Public License along
# with this program. If not, see <http://www.gnu.org/licenses/>.
#
# (C) 2020-2026 ERISCO, LLC
#
# For contributors and individual authors please refer to CONTRIBUTORS.MD.

"""Post-generation patch for element-carried xs:list members (HTOO-377).

xscgen (XmlSchemaClassGenerator) maps an element-carried xs:list of tokens
to List<T> with [XmlElement] -- i.e. repeated elements, which is
wire-incompatible with existing world data (one element, space-separated
tokens). XmlSerializer produces the space-separated shape for exactly two
property types: a [Flags] enum, or a string.

Two treatments, chosen by the member's semantics:

  * SITES -- token sets with flags semantics. The five enum types are
    maintained by hand in src/Enums/ as [Flags] enums and the generated
    member properties are repointed at them.
  * BRIDGE_SITES -- lists where order/repetition matter (StatAlloc) or the
    tokens are open-ended (Access names). The typed List<T> property stays
    as the public API but becomes [XmlIgnore]; a hidden string property
    carries the wire format, converting on get/set.

Run after xscgen output is copied into src/Objects/:

    python3 tools/patch-flags-enums.py

The script is strict: every expected replacement must occur exactly the
expected number of times, or it exits nonzero having written nothing. A
failure means the generator's output shape changed and the patch (or the
XSD) needs a human look -- do not ship the unpatched output.
"""

import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OBJECTS = ROOT / "src" / "Objects"

# Generated enum types xscgen emits for the xs:list token restrictions,
# superseded by the maintained [Flags] enums in src/Enums/.
LIST_ENUM_FILES = [
    "CreatureConditionList.cs",
    "DamageFlagsList.cs",
    "SpawnFlagsList.cs",
    "MapFlagsList.cs",
    "ItemFlagsList.cs",
]

# (member, maintained enum). Flags semantics: set membership only.
SITES = {
    "Conditions.cs": [("Set", "CreatureCondition"), ("Unset", "CreatureCondition")],
    "CastableDamage.cs": [("Flags", "DamageFlags")],
    "StatusDamage.cs": [("Flags", "DamageFlags")],
    "Map.cs": [("Flags", "MapFlags")],
    "ItemProperties.cs": [("Flags", "ItemFlags")],
    "VariantProperties.cs": [("Flags", "ItemFlags")],
    "Spawn.cs": [("Flags", "SpawnFlags")],
}

# (member, list item type). List semantics: order and repetition
# meaningful, or open-ended tokens. Typed API + string wire bridge.
BRIDGE_SITES = {
    "CreatureBehaviorSet.cs": [("StatAlloc", "StatType")],
    "Access.cs": [("Privileged", "string"), ("Reserved", "string")],
}

# Defaults-bearing children: the XSD default values on these types' members
# carry game semantics (mastery uses, per-class level caps), and the server
# reads them unguarded at 20+ sites. An absent element means "all defaults",
# so the member is materialized. Data-record children deliberately are NOT
# listed -- absent means null for those (see HTOO-377). ServerConfig's
# equivalents (Constants/Formulas/ApiEndpoints) live in its Init() instead.
INIT_SITES = {
    "Castable.cs": [("MaxLevel", "MaxLevel"), ("Mastery", "CastableMastery")],
}

errors = []
patched: dict[Path, str] = {}


def sub_counted(pattern: str, repl: str, text: str, expected: int, ctx: str) -> str:
    new, n = re.subn(pattern, repl, text)
    if n != expected:
        errors.append(f"{ctx}: expected {expected} replacement(s), got {n}: /{pattern}/")
    return new


def bridge_property(member: str, item: str, list_type: str) -> str:
    """The hidden string property that carries the xs:list wire format."""
    if item == "string":
        parse = ("value.Split((char[])null, System.StringSplitOptions.RemoveEmptyEntries)"
                 ".ToList()")
    else:
        parse = ("value.Split((char[])null, System.StringSplitOptions.RemoveEmptyEntries)"
                 f".Select(System.Enum.Parse<{item}>).ToList()")
    return f"""
        /// <summary>
        /// Wire carrier for {member}: the XSD type is an xs:list, one element
        /// containing space-separated tokens, which XmlSerializer cannot map
        /// to a typed collection. Use {member} instead.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlElementAttribute("{member}")]
        public string {member}XmlList
        {{
            get
            {{
                return ((this.{member} != null) && (this.{member}.Count != 0))
                    ? string.Join(" ", this.{member}) : null;
            }}
            set
            {{
                this.{member} = string.IsNullOrWhiteSpace(value)
                    ? new {list_type}()
                    : {parse};
            }}
        }}
"""


for fname, members in SITES.items():
    path = OBJECTS / fname
    text = path.read_text()

    for member, enum in members:
        list_type = rf"System\.Collections\.Generic\.List<(?:Hybrasyl\.Xml\.Objects\.)?{enum}List>"
        field = "_" + member[0].lower() + member[1:]

        # Constructor init: List allocation -> enum default. Spawn's XSD
        # attribute declares default="Active"; a missing attribute must
        # yield Active, so the field init carries the default (matching
        # the old generator's output).
        init_value = "SpawnFlags.Active" if enum == "SpawnFlags" else f"default({enum})"
        text = sub_counted(
            rf"this\.{field} = new {list_type}\(\);",
            f"this.{field} = {init_value};",
            text, 1, f"{fname}:{member} ctor init",
        )

        # Backing field + property declaration, scoped by name: two
        # members can share one enum type (Conditions.Set/Unset), so a
        # blanket type replacement would cross-contaminate.
        text = sub_counted(
            rf"private {list_type} {field};",
            f"private {enum} {field};",
            text, 1, f"{fname}:{member} field type",
        )
        text = sub_counted(
            rf"public {list_type} {member}\b",
            f"public {enum} {member}",
            text, 1, f"{fname}:{member} property type",
        )

        # The "*Specified is the collection non-empty" body no longer
        # compiles against an enum; "has any flag set" preserves its
        # meaning (skip serializing an empty value -- all these elements
        # and attributes are optional in the XSD except Damage.Flags,
        # which xscgen marks Required and gives no Specified member).
        text = sub_counted(
            rf"\(\(this\.{member} != null\)\s*\r?\n\s*&& \(this\.{member}\.Count != 0\)\);",
            f"(this.{member} != default({enum}));",
            text,
            1 if fname not in ("CastableDamage.cs", "StatusDamage.cs") else 0,
            f"{fname}:{member} Specified body",
        )

    if fname == "Spawn.cs":
        # Restore the old generator's serialize-side default handling:
        # omit the attribute when the value is the schema default.
        text = sub_counted(
            r'(\[System\.Xml\.Serialization\.XmlAttributeAttribute\("Flags"\)\])',
            "[System.ComponentModel.DefaultValueAttribute(SpawnFlags.Active)]\n        \\1",
            text, 1, "Spawn.cs DefaultValue attribute",
        )

    patched[path] = text

for fname, members in BRIDGE_SITES.items():
    path = OBJECTS / fname
    text = patched.get(path, path.read_text())

    for member, item in members:
        item_re = "string" if item == "string" else rf"(?:Hybrasyl\.Xml\.Objects\.)?{item}"
        list_re = rf"System\.Collections\.Generic\.List<{item_re}>"
        field = "_" + member[0].lower() + member[1:]

        # The typed property leaves the wire; the bridge takes its place,
        # inserted directly after the property block so the serialized
        # element keeps its position in the XSD sequence.
        text = sub_counted(
            rf'\[System\.Xml\.Serialization\.XmlElementAttribute\("{member}"\)\]'
            rf"(?=\r?\n\s*public {list_re} {member}\b)",
            "[System.Xml.Serialization.XmlIgnoreAttribute()]",
            text, 1, f"{fname}:{member} XmlElement -> XmlIgnore",
        )
        # Concrete C# spelling for the bridge's setter, taken from the
        # generated field initializer so qualification matches the file.
        m = re.search(rf"this\.{field} = new (System\.Collections\.Generic\.List<[^>]+>)\(\);", text)
        if m is None:
            errors.append(f"{fname}:{member}: could not find ctor init to derive list type")
            continue
        text = sub_counted(
            rf"(public {list_re} {member}\r?\n"
            rf"\s*\{{\r?\n"
            rf"\s*get\r?\n\s*\{{\r?\n\s*return {field};\r?\n\s*\}}\r?\n"
            rf"\s*set\r?\n\s*\{{\r?\n\s*{field} = value;\r?\n\s*\}}\r?\n"
            rf"\s*\}})",
            r"\1" + bridge_property(member, item, m.group(1)).rstrip("\n"),
            text, 1, f"{fname}:{member} bridge insertion",
        )

    # The bridge parses with LINQ; generated files do not import it.
    if "using System.Linq;" not in text:
        text = sub_counted(
            r"(namespace Hybrasyl\.Xml\.Objects\r?\n\{)",
            r"using System.Linq;\n\n\1",
            text, 1, f"{fname} using System.Linq",
        )
    patched[path] = text

for fname, members in INIT_SITES.items():
    path = OBJECTS / fname
    text = patched.get(path, path.read_text())
    for member, mtype in members:
        text = sub_counted(
            rf"public {mtype} {member} \{{ get; set; \}}",
            f"public {mtype} {member} {{ get; set; }} = new {mtype}();",
            text, 1, f"{fname}:{member} defaults init",
        )
    patched[path] = text

for fname in LIST_ENUM_FILES:
    if not (OBJECTS / fname).exists():
        errors.append(f"{fname}: expected generated file to delete, not found")

# All-or-nothing: a failed check means the generator's output shape has
# drifted, and a partial patch is worse than none -- write nothing.
if errors:
    print("patch-flags-enums: FAILED (no files modified)", file=sys.stderr)
    for e in errors:
        print(f"  {e}", file=sys.stderr)
    sys.exit(1)

for path, text in patched.items():
    path.write_text(text)
for fname in LIST_ENUM_FILES:
    (OBJECTS / fname).unlink()

print(f"patch-flags-enums: patched {len(patched)} files, removed {len(LIST_ENUM_FILES)} generated enums")
