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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class CastableDamage : HybrasylEntity<CastableDamage>
{
    #region Private fields
    private DamageFlags _flags;
    private SimpleQuantity _simple;
    private string _formula;
    private DamageType _type;
    #endregion
    
    public CastableDamage()
    {
        _simple = new SimpleQuantity();
    }
    
    public DamageFlags Flags
    {
        get
        {
            return _flags;
        }
        set
        {
            _flags = value;
        }
    }
    
    public SimpleQuantity Simple
    {
        get
        {
            return _simple;
        }
        set
        {
            _simple = value;
        }
    }
    
    public string Formula
    {
        get
        {
            return _formula;
        }
        set
        {
            _formula = value;
        }
    }
    
    [XmlAttribute]
    public DamageType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
}
}
#pragma warning restore
