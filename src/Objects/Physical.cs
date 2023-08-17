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
public partial class Physical : HybrasylEntity<Physical>
{
    #region Private fields
    private float _value;
    private float _weight;
    private float _durability;
    #endregion
    
    public Physical()
    {
        _value = ((float)(1F));
        _weight = ((float)(1F));
        _durability = ((float)(1F));
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(float), "1")]
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(float), "1")]
    public float Weight
    {
        get
        {
            return _weight;
        }
        set
        {
            _weight = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(float), "1")]
    public float Durability
    {
        get
        {
            return _durability;
        }
        set
        {
            _durability = value;
        }
    }
}
}
#pragma warning restore
