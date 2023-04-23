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
[XmlTypeAttribute(AnonymousType=true, Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class NpcInventoryItem : HybrasylEntity<NpcInventoryItem>
{
    #region Private fields
    private uint _quantity;
    private uint _refresh;
    private string _value;
    #endregion
    
    public NpcInventoryItem()
    {
        _quantity = ((uint)(0));
        _refresh = ((uint)(0));
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Quantity
    {
        get
        {
            return _quantity;
        }
        set
        {
            _quantity = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Refresh
    {
        get
        {
            return _refresh;
        }
        set
        {
            _refresh = value;
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