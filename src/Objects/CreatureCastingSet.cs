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
public partial class CreatureCastingSet : HybrasylEntity<CreatureCastingSet>
{
    #region Private fields
    private List<CreatureCastable> _castable;
    private RotationType _type;
    private string _categories;
    private int _interval;
    private CreatureTargetPriority _targetPriority;
    private int _healthPercentage;
    private bool _random;
    #endregion
    
    public CreatureCastingSet()
    {
        _interval = 15;
        _targetPriority = CreatureTargetPriority.HighThreat;
        _healthPercentage = 0;
        _random = true;
    }
    
    [XmlElement("Castable")]
    public List<CreatureCastable> Castable
    {
        get
        {
            return _castable;
        }
        set
        {
            _castable = value;
        }
    }
    
    [XmlAttribute]
    public RotationType Type
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
    
    [XmlAttribute(DataType="token")]
    public string Categories
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
    
    [XmlAttribute]
    [DefaultValue(15)]
    public int Interval
    {
        get
        {
            return _interval;
        }
        set
        {
            _interval = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(CreatureTargetPriority.HighThreat)]
    public CreatureTargetPriority TargetPriority
    {
        get
        {
            return _targetPriority;
        }
        set
        {
            _targetPriority = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int HealthPercentage
    {
        get
        {
            return _healthPercentage;
        }
        set
        {
            _healthPercentage = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool Random
    {
        get
        {
            return _random;
        }
        set
        {
            _random = value;
        }
    }
}
}
#pragma warning restore
