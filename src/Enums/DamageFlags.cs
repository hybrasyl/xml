// Maintained by hand -- not generated (see HTOO-377).
//
// These five types are element- or attribute-carried xs:list-of-token
// restrictions in the XSD. XmlSerializer round-trips their wire format
// (one value, space-separated tokens) only for a [Flags] enum, which
// xscgen does not synthesize; tools/patch-flags-enums.py repoints the
// generated members here after each regeneration. Keep members and
// values in sync with the corresponding simpleType in src/XSD/.
namespace Hybrasyl.Xml.Objects
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
public enum DamageFlags
{
    None = 1,
    NoResistance = 2,
    NoThreat = 4,
    Nonlethal = 8,
    NoDodge = 16,
    NoCrit = 32,
    NoElement = 64,
}
}
#pragma warning restore
