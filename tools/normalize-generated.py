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

"""Make the generated files the same on every platform (HXML-26).

Two parts of xscgen's output depend on the machine:

- Each header records the xscgen command line. regenerate.sh passes the
  relative output folder obj/xscgen, so the header is the same on every
  machine. This script writes src/Objects in its place, where the files are.
- xscgen writes CRLF on every line on Windows, and CRLF only inside property
  bodies on Linux. patch-flags-enums.py writes the platform's own line ending.
  This script ends every line in src/Objects and src/Enums with LF.

It runs last, so a regeneration gives the same bytes on every platform.
.gitattributes keeps the files LF in a checkout. A generated header with
neither output path means regenerate.sh changed: the script then stops and
writes nothing.
"""

import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
HEADER = b"// xscgen src/XSD/"
GENERATED_OUT = b" -o obj/xscgen "
COMMITTED_OUT = b" -o src/Objects "

normalized = {}
errors = []
for path in sorted([*ROOT.glob("src/Objects/*.cs"), *ROOT.glob("src/Enums/*.cs")]):
    data = path.read_bytes()
    out = data.replace(b"\r\n", b"\n")
    if HEADER in out:
        if GENERATED_OUT in out:
            out = out.replace(GENERATED_OUT, COMMITTED_OUT, 1)
        elif COMMITTED_OUT not in out:
            errors.append(path.relative_to(ROOT))
    if out != data:
        normalized[path] = out

if errors:
    print("normalize-generated: FAILED (no files modified). No known -o path in:", file=sys.stderr)
    for e in errors:
        print(f"  {e}", file=sys.stderr)
    sys.exit(1)

for path, out in normalized.items():
    path.write_bytes(out)

print(f"normalize-generated: normalized {len(normalized)} file(s)")
