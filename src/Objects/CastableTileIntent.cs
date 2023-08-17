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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class CastableTileIntent : HybrasylEntity<CastableTileIntent>
{
    #region Private fields
    private IntentDirection _direction;
    private sbyte _relativeX;
    private sbyte _relativeY;
    private VisualEffectType _visualEffect;
    #endregion
    
    public CastableTileIntent()
    {
        _direction = IntentDirection.None;
        _relativeX = ((sbyte)(0));
        _relativeY = ((sbyte)(0));
        _visualEffect = VisualEffectType.Targets;
    }
    
    [XmlAttribute]
    [DefaultValue(IntentDirection.None)]
    public IntentDirection Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(sbyte), "0")]
    public sbyte RelativeX
    {
        get
        {
            return _relativeX;
        }
        set
        {
            _relativeX = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(sbyte), "0")]
    public sbyte RelativeY
    {
        get
        {
            return _relativeY;
        }
        set
        {
            _relativeY = value;
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
