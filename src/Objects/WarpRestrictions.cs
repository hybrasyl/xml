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
public partial class WarpRestrictions : HybrasylEntity<WarpRestrictions>
{
    #region Private fields
    private byte _minLev;
    private byte _maxLev;
    private byte _minAb;
    private byte _maxAb;
    private bool _mobUse;
    private string _requireCookie;
    private string _prohibitCookie;
    private string _message;
    #endregion
    
    public WarpRestrictions()
    {
        _minLev = ((byte)(0));
        _maxLev = ((byte)(255));
        _minAb = ((byte)(0));
        _maxAb = ((byte)(255));
        _mobUse = true;
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte MinLev
    {
        get
        {
            return _minLev;
        }
        set
        {
            _minLev = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "255")]
    public byte MaxLev
    {
        get
        {
            return _maxLev;
        }
        set
        {
            _maxLev = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte MinAb
    {
        get
        {
            return _minAb;
        }
        set
        {
            _minAb = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "255")]
    public byte MaxAb
    {
        get
        {
            return _maxAb;
        }
        set
        {
            _maxAb = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool MobUse
    {
        get
        {
            return _mobUse;
        }
        set
        {
            _mobUse = value;
        }
    }
    
    [XmlAttribute]
    public string RequireCookie
    {
        get
        {
            return _requireCookie;
        }
        set
        {
            _requireCookie = value;
        }
    }
    
    [XmlAttribute]
    public string ProhibitCookie
    {
        get
        {
            return _prohibitCookie;
        }
        set
        {
            _prohibitCookie = value;
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
}
}
#pragma warning restore
