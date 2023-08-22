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
public partial class AddStatus : HybrasylEntity<AddStatus>
{
    #region Private fields
    private int _duration;
    private float _intensity;
    private int _tick;
    private string _removeChance;
    private bool _persistDeath;
    private string _value;
    #endregion
    
    public AddStatus()
    {
        _duration = 0;
        _intensity = ((float)(1F));
        _tick = 0;
        _persistDeath = false;
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
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
    [DefaultValue(typeof(float), "1")]
    public float Intensity
    {
        get
        {
            return _intensity;
        }
        set
        {
            _intensity = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
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
    public string RemoveChance
    {
        get
        {
            return _removeChance;
        }
        set
        {
            _removeChance = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool PersistDeath
    {
        get
        {
            return _persistDeath;
        }
        set
        {
            _persistDeath = value;
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
