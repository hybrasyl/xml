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
public partial class EquipmentRestriction : HybrasylEntity<EquipmentRestriction>
{
    #region Private fields
    private WeaponType _weaponType;
    private EquipmentSlot _slot;
    private RestrictionType _restrictionType;
    private string _message;
    private string _value;
    #endregion
    
   
    [XmlAttribute]
    public WeaponType WeaponType
    {
        get
        {
            return _weaponType;
        }
        set
        {
            _weaponType = value;
        }
    }
    
    [XmlAttribute]
    public EquipmentSlot Slot
    {
        get
        {
            return _slot;
        }
        set
        {
            _slot = value;
        }
    }
    
    [XmlAttribute]
    public RestrictionType RestrictionType
    {
        get
        {
            return _restrictionType;
        }
        set
        {
            _restrictionType = value;
        }
    }
    
    [XmlAttribute]
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
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
