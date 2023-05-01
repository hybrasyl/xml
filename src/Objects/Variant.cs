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
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class Variant : HybrasylEntity<Variant>
{
    #region Private fields
    private string _name;
    private string _modifier;
    private string _comment;
    private VariantProperties _properties;
    #endregion
    
    public Variant()
    {
        _properties = new VariantProperties();
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Modifier
    {
        get
        {
            return _modifier;
        }
        set
        {
            _modifier = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Comment
    {
        get
        {
            return _comment;
        }
        set
        {
            _comment = value;
        }
    }
    
    public VariantProperties Properties
    {
        get
        {
            return _properties;
        }
        set
        {
            _properties = value;
        }
    }
}
}
#pragma warning restore
