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
public partial class NpcRoleVendItem : HybrasylEntity<NpcRoleVendItem>
{
    #region Private fields
    private string _name;
    private int _quantity;
    private int _restock;
    private string _tab;
    #endregion
    
    [XmlAttribute]
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
    
    [XmlAttribute]
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
    
    [XmlAttribute]
    public int Restock
    {
        get
        {
            return _restock;
        }
        set
        {
            _restock = value;
        }
    }
    
    [XmlAttribute]
    public string Tab
    {
        get
        {
            return _tab;
        }
        set
        {
            _tab = value;
        }
    }
}
}
#pragma warning restore
