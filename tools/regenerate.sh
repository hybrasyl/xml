#!/usr/bin/env bash
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

# Regenerate src/Objects from src/XSD (HTOO-377).
#
# Requires: dotnet tool install -g dotnet-xscgen
#
# Generation is two steps: xscgen emits the model, then
# patch-flags-enums.py applies the wire-format fixups XmlSerializer needs
# but xscgen cannot express from the schema alone. The patch step is
# strict -- if generator output drifts, it fails and writes nothing.
set -euo pipefail

cd "$(dirname "$0")/.."

XSCGEN="${XSCGEN:-$HOME/.dotnet/tools/xscgen}"
if ! command -v "$XSCGEN" >/dev/null 2>&1; then
    echo "xscgen not found at $XSCGEN -- run: dotnet tool install -g dotnet-xscgen" >&2
    exit 1
fi

NAMESPACE='http://www.hybrasyl.com/XML/Hybrasyl/2020-02=Hybrasyl.Xml.Objects'
OUT="$(mktemp -d)"
trap 'rm -rf "$OUT"' EXIT

# Every schema is passed individually, and there is deliberately no aggregate
# schema that xs:includes the others: passing one alongside its own includes
# defines every type twice, which xscgen reports by exiting 4 while still
# emitting correct output -- a nonzero exit we would then have to ignore.
# shellcheck disable=SC2046  # deliberate word splitting over the schema list
"$XSCGEN" $(ls src/XSD/*.xsd) \
    -o "$OUT" \
    -n "$NAMESPACE" \
    --separateFiles \
    --collectionType='System.Collections.Generic.List`1' \
    --collectionSettersMode=Public \
    --enumCollection

cp "$OUT/Hybrasyl.Xml.Objects/"*.cs src/Objects/

# xscgen records its own invocation in every file's header, including the
# absolute -o path. Normalize it so regenerating on a different machine
# produces no diff.
sed -i 's| -o /[^ ]*| -o src/Objects|' src/Objects/*.cs

python3 tools/patch-flags-enums.py

# Restore each enum member's allocated value. xscgen emits none, so a member's
# number would otherwise be its position in the XSD -- and several of these
# enums are cast to a byte and put on the wire.
python3 tools/pin-enum-values.py

echo "regenerate: done -- build and run the test suite before committing"
