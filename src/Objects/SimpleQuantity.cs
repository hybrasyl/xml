// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 6.0.142.0. www.xsd2code.com
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
public partial class SimpleQuantity : HybrasylEntity<SimpleQuantity>
{
    #region Private fields
    private uint _min;
    private uint _max;
    private uint _value;
    #endregion
    
    public SimpleQuantity()
    {
        _min = ((uint)(0));
        _max = ((uint)(0));
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Min
    {
        get
        {
            return _min;
        }
        set
        {
            _min = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Max
    {
        get
        {
            return _max;
        }
        set
        {
            _max = value;
        }
    }
    
    [XmlTextAttribute]
    public uint Value
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
