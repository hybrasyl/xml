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
public partial class Creature : HybrasylEntity<Creature>
{
    #region Private fields
    private string _description;
    private LootList _loot;
    private CreatureHostilitySettings _hostility;
    private List<CreatureCookie> _setCookies;
    private List<Creature> _types;
    private string _name;
    private ushort _sprite;
    private string _behaviorSet;
    private int _minDmg;
    private int _maxDmg;
    private byte _assailSound;
    #endregion
    
    public Creature()
    {
        _minDmg = 0;
        _maxDmg = 0;
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }
    
    public LootList Loot
    {
        get
        {
            return _loot;
        }
        set
        {
            _loot = value;
        }
    }
    
    public CreatureHostilitySettings Hostility
    {
        get
        {
            return _hostility;
        }
        set
        {
            _hostility = value;
        }
    }
    
    [XmlArrayItemAttribute("Cookie", IsNullable=false)]
    public List<CreatureCookie> SetCookies
    {
        get
        {
            return _setCookies;
        }
        set
        {
            _setCookies = value;
        }
    }
    
    [XmlArrayItemAttribute("Type", IsNullable=false)]
    public List<Creature> Types
    {
        get
        {
            return _types;
        }
        set
        {
            _types = value;
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
    public ushort Sprite
    {
        get
        {
            return _sprite;
        }
        set
        {
            _sprite = value;
        }
    }
    
    [XmlAttribute]
    public string BehaviorSet
    {
        get
        {
            return _behaviorSet;
        }
        set
        {
            _behaviorSet = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int MinDmg
    {
        get
        {
            return _minDmg;
        }
        set
        {
            _minDmg = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int MaxDmg
    {
        get
        {
            return _maxDmg;
        }
        set
        {
            _maxDmg = value;
        }
    }
    
    [XmlAttribute]
    public byte AssailSound
    {
        get
        {
            return _assailSound;
        }
        set
        {
            _assailSound = value;
        }
    }
}
}
#pragma warning restore
