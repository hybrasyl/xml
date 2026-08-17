// This file is part of Project Hybrasyl.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
//
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
//
// (C) 2020-2026 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

// Maintained by hand -- not generated (see HTOO-377).
//
// These five types are element- or attribute-carried xs:list-of-token
// restrictions in the XSD. XmlSerializer round-trips their wire format
// (one value, space-separated tokens) only for a [Flags] enum, which
// xscgen does not synthesize; tools/patch-flags-enums.py repoints the
// generated members here after each regeneration. Keep members and
// values in sync with the corresponding simpleType in src/XSD/.
namespace Hybrasyl.Xml.Enums
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[System.FlagsAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[XmlTypeAttribute(AnonymousType=true, Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public enum MapFlags
{
    Snow = 1,
    Rain = 2,
    Dark = 4,
    NoMap = 8,
    Winter = 16,
}
}
#pragma warning restore
