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
public partial class RemoveStatus : HybrasylEntity<RemoveStatus>
{
    #region Private fields
    private bool _isCategory;
    private int _quantity;
    private string _value;
    #endregion
    
    public RemoveStatus()
    {
        _isCategory = false;
        _quantity = 1;
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool IsCategory
    {
        get
        {
            return _isCategory;
        }
        set
        {
            _isCategory = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(1)]
    public int Quantity
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
