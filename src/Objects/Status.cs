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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class Status : HybrasylEntity<Status>
{
    #region Private fields
    private List<Category> _categories;
    private List<CastRestriction> _castRestrictions;
    private StatusEffects _effects;
    private string _prohibitedMessage;
    private string _script;
    private int _duration;
    private int _tick;
    private ushort _icon;
    private string _name;
    private bool _removeOnDeath;
    #endregion
    
    public Status()
    {
        _categories = new List<Category>();
        _tick = 1;
        _removeOnDeath = true;
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<Category> Categories
    {
        get
        {
            return _categories;
        }
        set
        {
            _categories = value;
        }
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<CastRestriction> CastRestrictions
    {
        get
        {
            return _castRestrictions;
        }
        set
        {
            _castRestrictions = value;
        }
    }
    
    public StatusEffects Effects
    {
        get
        {
            return _effects;
        }
        set
        {
            _effects = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string ProhibitedMessage
    {
        get
        {
            return _prohibitedMessage;
        }
        set
        {
            _prohibitedMessage = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Script
    {
        get
        {
            return _script;
        }
        set
        {
            _script = value;
        }
    }
    
    [XmlAttribute]
    public int Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            _duration = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(1)]
    public int Tick
    {
        get
        {
            return _tick;
        }
        set
        {
            _tick = value;
        }
    }
    
    [XmlAttribute]
    public ushort Icon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
        }
    }
    
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
    [DefaultValue(true)]
    public bool RemoveOnDeath
    {
        get
        {
            return _removeOnDeath;
        }
        set
        {
            _removeOnDeath = value;
        }
    }
}
}
#pragma warning restore
