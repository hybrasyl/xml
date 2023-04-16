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
public partial class ItemRestrictions : HybrasylXmlEntity<ItemRestrictions>
{
    #region Private fields
    private RestrictionsLevel _level;
    private RestrictionsAb _ab;
    private Class _class;
    private Gender _gender;
    private List<string> _castables;
    private List<SlotRestriction> _slotRestrictions;
    #endregion
    
    public ItemRestrictions()
    {
        _class = Class.Peasant;
        _gender = Gender.Neutral;
    }
    
    public RestrictionsLevel Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    
    public RestrictionsAb Ab
    {
        get
        {
            return _ab;
        }
        set
        {
            _ab = value;
        }
    }
    
    [DefaultValue(Class.Peasant)]
    public Class Class
    {
        get
        {
            return _class;
        }
        set
        {
            _class = value;
        }
    }
    
    [DefaultValue(Gender.Neutral)]
    public Gender Gender
    {
        get
        {
            return _gender;
        }
        set
        {
            _gender = value;
        }
    }
    
    [XmlArrayItemAttribute("Castable", IsNullable=false)]
    public List<string> Castables
    {
        get
        {
            return _castables;
        }
        set
        {
            _castables = value;
        }
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<SlotRestriction> SlotRestrictions
    {
        get
        {
            return _slotRestrictions;
        }
        set
        {
            _slotRestrictions = value;
        }
    }
}
}
#pragma warning restore
