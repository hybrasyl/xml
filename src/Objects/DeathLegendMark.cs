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
public partial class DeathLegendMark : HybrasylEntity<DeathLegendMark>
{
    #region Private fields
    private string _prefix;
    private bool _increment;
    private string _value;
    #endregion
    
    public DeathLegendMark()
    {
        _prefix = "deaths";
        _increment = true;
    }
    
    [XmlAttribute]
    [DefaultValue("deaths")]
    public string Prefix
    {
        get
        {
            return _prefix;
        }
        set
        {
            _prefix = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool Increment
    {
        get
        {
            return _increment;
        }
        set
        {
            _increment = value;
        }
    }
    
    [XmlTextAttribute]
    public string Value
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
}
}
#pragma warning restore
