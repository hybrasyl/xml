// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 6.0.74.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class CastableSquareIntent : HybrasylEntity<CastableSquareIntent>
{
    #region Private fields
    private byte _side;
    private VisualEffectType _visualEffect;
    #endregion
    
    public CastableSquareIntent()
    {
        _visualEffect = VisualEffectType.Targets;
    }
    
    [XmlAttribute]
    public byte Side
    {
        get
        {
            return _side;
        }
        set
        {
            _side = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(VisualEffectType.Targets)]
    public VisualEffectType VisualEffect
    {
        get
        {
            return _visualEffect;
        }
        set
        {
            _visualEffect = value;
        }
    }
}
}
#pragma warning restore
