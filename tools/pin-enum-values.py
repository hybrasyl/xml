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

"""Pin generated enum members to their allocated integer values.

xscgen emits enum members with no explicit value, so each member's number is
its position in the XSD. Several of these enums are cast to a byte and put on
the wire -- `(byte)user.Class` reaches a UserListEntry, `(byte)Gender` reaches
a UserAppearancePacket -- so reordering an xs:enumeration silently changes what
goes over the network, with no compile error and no diff at the call site.

This script makes the value explicit and, more importantly, makes it *stick*:
values come from tools/enum-values.json, not from declaration order, so moving
a member in the schema no longer moves its number.

The registry is authoritative. The script is strict, like patch-flags-enums.py:
a new enum, a new member, or a member that disappeared all stop the run and
write nothing, because each one is a decision a human should make explicitly.
Allocating a value is exactly as permanent as allocating an opcode.

    python3 tools/pin-enum-values.py              # apply the registry
    python3 tools/pin-enum-values.py --bootstrap  # rewrite it from current source
    python3 tools/pin-enum-values.py --check      # verify without writing
"""

import json
import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OBJECTS = ROOT / "src" / "Objects"
REGISTRY = ROOT / "tools" / "enum-values.json"

ENUM_DECL = re.compile(r"^\s*public enum (\w+)\s*$")
CLASS_DECL = re.compile(r"^\s*public (partial )?(class|interface|struct|record)\b")
MEMBER = re.compile(r"^(\s*)(\w+)(\s*=\s*-?\d+)?,\s*$")


def read(path):
    """Read without newline translation, so a CRLF checkout survives a rewrite."""
    with open(path, encoding="utf-8", newline="") as f:
        return f.read()


def write(path, text):
    with open(path, "w", encoding="utf-8", newline="") as f:
        f.write(text)


def enum_files():
    """Generated files whose only top-level type is an enum, in stable order."""
    for path in sorted(OBJECTS.glob("*.cs")):
        lines = read(path).splitlines()
        if any(ENUM_DECL.match(l) for l in lines) and not any(
            CLASS_DECL.match(l) for l in lines
        ):
            yield path, lines


def parse(lines):
    """(enum name, [(line index, indent, member, existing value or None)])."""
    name = next(ENUM_DECL.match(l).group(1) for l in lines if ENUM_DECL.match(l))
    members = []
    for i, line in enumerate(lines):
        m = MEMBER.match(line)
        if not m:
            continue
        value = None
        if m.group(3):
            value = int(m.group(3).split("=")[1])
        members.append((i, m.group(1), m.group(2), value))
    return name, members


def bootstrap():
    registry = {}
    for path, lines in enum_files():
        name, members = parse(lines)
        registry[name] = {member: i for i, (_, _, member, _) in enumerate(members)}
    REGISTRY.write_text(json.dumps(registry, indent=2, sort_keys=True) + "\n")
    total = sum(len(v) for v in registry.values())
    print(f"pin-enum-values: bootstrapped {len(registry)} enums, {total} members")


def apply(check_only):
    if not REGISTRY.exists():
        sys.exit(f"pin-enum-values: {REGISTRY} not found -- run with --bootstrap first")

    registry = json.loads(REGISTRY.read_text())
    errors, edits = [], []

    for path, lines in enum_files():
        name, members = parse(lines)
        pinned = registry.get(name)

        if pinned is None:
            errors.append(
                f"{name}: not in the registry. A new enum needs values allocated in "
                f"{REGISTRY.name}; start at 0 and never reuse a number."
            )
            continue

        for _, _, member, existing in members:
            if member not in pinned:
                nxt = max(pinned.values(), default=-1) + 1
                errors.append(
                    f"{name}.{member}: new member, not in the registry. Allocate it "
                    f"explicitly (next free value is {nxt}) -- appending is safe, "
                    f"renumbering an existing member is a wire change."
                )
            elif existing is not None and existing != pinned[member]:
                errors.append(
                    f"{name}.{member}: source says {existing}, registry says "
                    f"{pinned[member]}. The registry wins; do not hand-edit values."
                )

        present = {member for _, _, member, _ in members}
        for member in pinned:
            if member not in present:
                errors.append(
                    f"{name}.{member}: pinned but no longer generated. Remove it from "
                    f"{REGISTRY.name} deliberately, and leave its number unused."
                )

        if errors:
            continue

        newline = "\r\n" if "\r\n" in read(path) else "\n"
        updated = list(lines)
        for index, indent, member, _ in members:
            updated[index] = f"{indent}{member} = {pinned[member]},"
        if updated != lines:
            edits.append((path, newline.join(updated) + newline))

    if errors:
        print("pin-enum-values: FAILED (no files modified)", file=sys.stderr)
        for e in errors:
            print(f"  {e}", file=sys.stderr)
        sys.exit(1)

    if check_only:
        print(f"pin-enum-values: registry matches source ({len(edits)} file(s) would change)")
        return

    for path, text in edits:
        write(path, text)
    print(f"pin-enum-values: pinned {len(edits)} file(s) from {REGISTRY.name}")


if __name__ == "__main__":
    if "--bootstrap" in sys.argv:
        bootstrap()
    else:
        apply("--check" in sys.argv)
