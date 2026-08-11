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
public enum CreatureCondition
{
    Stun = 1,
    Sleep = 2,
    Root = 4,
    Blind = 8,
    Coma = 16,
    Poison = 32,
    Sight = 64,
    Invisible = 128,
    Mute = 256,
    Invulnerable = 512,
    Charm = 1024,
    ProhibitItemUse = 2048,
    ProhibitEquipChange = 4096,
    ProhibitSpeech = 8192,
    ProhibitWhisper = 16384,
    ProhibitShout = 32768,
    Disoriented = 65536,
    Disarm = 131072,
    Fear = 262144,
    ProhibitHpRegen = 524288,
    ProhibitMpRegen = 1048576,
    ProhibitHpIncrease = 2097152,
    ProhibitMpIncrease = 4194304,
    ProhibitMpDecrease = 8388608,
    ProhibitXpIncrease = 16777216,
}
}
#pragma warning restore
