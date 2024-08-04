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
public partial class NpcRoleRepair : HybrasylEntity<NpcRoleRepair>
{
    #region Private fields
    private string _nation;
    private int _discount;
    private List<NpcRepairType> _type;
    #endregion
    
    public NpcRoleRepair()
    {
        _type = new List<NpcRepairType>();
    }
    
    [XmlAttribute]
    public string Nation
    {
        get
        {
            return _nation;
        }
        set
        {
            _nation = value;
        }
    }
    
    [XmlAttribute]
    public int Discount
    {
        get
        {
            return _discount;
        }
        set
        {
            _discount = value;
        }
    }
    
    [XmlAttribute]
    public List<NpcRepairType> Type
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
