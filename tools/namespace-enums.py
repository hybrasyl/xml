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

"""Move generated enums into the Hybrasyl.Xml.Enums namespace.

Every type in these schemas shares one XML namespace, so xscgen puts every
type in one C# namespace. The enums do not belong there: they are shared
vocabulary with a different maintenance contract from the data model, since
their values are allocated in enum-values.json and are permanent, while a
generated class has no such promise.

Two rewrites, because xscgen emits fully-qualified type references:

  * the namespace declaration in each enum-only file, and
  * every `Hybrasyl.Xml.Objects.<Enum>` reference in the generated model.

Consumers pick the namespace up with one line -- `<Using Include=
"Hybrasyl.Xml.Enums" />` in the project file -- rather than a using per file.

Strict, like the other post-generation steps: an enum in the registry that
this cannot find, or a leftover qualified reference, stops the run.

    python3 tools/namespace-enums.py           # apply
    python3 tools/namespace-enums.py --check   # verify without writing
"""

import json
import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OBJECTS = ROOT / "src" / "Objects"
HAND_WRITTEN = ROOT / "src" / "Enums"
REGISTRY = ROOT / "tools" / "enum-values.json"

OLD_NS = "Hybrasyl.Xml.Objects"
NEW_NS = "Hybrasyl.Xml.Enums"

ENUM_DECL = re.compile(r"^\s*public enum (\w+)\s*$")
CLASS_DECL = re.compile(r"^\s*public (partial )?(class|interface|struct|record)\b")


def read(path):
    with open(path, encoding="utf-8", newline="") as f:
        return f.read()


def write(path, text):
    with open(path, "w", encoding="utf-8", newline="") as f:
        f.write(text)


def enum_names():
    """Every enum type name: the generated registry plus the hand-maintained flags."""
    names = set(json.loads(REGISTRY.read_text()))
    for path in HAND_WRITTEN.glob("*.cs"):
        names.update(ENUM_DECL.match(l).group(1)
                     for l in read(path).splitlines() if ENUM_DECL.match(l))
    return names


def is_enum_only(text):
    lines = text.splitlines()
    return (any(ENUM_DECL.match(l) for l in lines)
            and not any(CLASS_DECL.match(l) for l in lines))


def main(check_only):
    names = enum_names()
    qualified = re.compile(r"%s\.(%s)\b" % (re.escape(OLD_NS), "|".join(sorted(names))))
    edits, declared = [], set()

    for path in sorted(list(OBJECTS.glob("*.cs")) + list(HAND_WRITTEN.glob("*.cs"))):
        text = read(path)
        updated = text

        if is_enum_only(text):
            updated = updated.replace(f"namespace {OLD_NS}", f"namespace {NEW_NS}")
            declared.update(ENUM_DECL.match(l).group(1)
                            for l in text.splitlines() if ENUM_DECL.match(l))

        updated = qualified.sub(rf"{NEW_NS}.\1", updated)

        if updated != text:
            edits.append((path, updated))

    missing = names - declared
    if missing:
        sys.exit("namespace-enums: FAILED (no files modified)\n  "
                 + f"declared nowhere: {', '.join(sorted(missing))}")

    if check_only:
        print(f"namespace-enums: {len(edits)} file(s) would change")
        return

    for path, text in edits:
        write(path, text)

    leftover = [p.name for p in OBJECTS.glob("*.cs") if qualified.search(read(p))]
    if leftover:
        sys.exit(f"namespace-enums: FAILED -- qualified references remain in {leftover}")

    print(f"namespace-enums: moved {len(declared)} enums, rewrote {len(edits)} file(s)")


if __name__ == "__main__":
    main("--check" in sys.argv)
