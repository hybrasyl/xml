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
[XmlTypeAttribute(AnonymousType=true, Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class ClassRequirementPhysical : HybrasylEntity<ClassRequirementPhysical>
{
    #region Private fields
    private byte _str;
    private byte _int;
    private byte _wis;
    private byte _con;
    private byte _dex;
    private uint _hp;
    private uint _mp;
    #endregion
    
    public ClassRequirementPhysical()
    {
        _str = ((byte)(0));
        _int = ((byte)(0));
        _wis = ((byte)(0));
        _con = ((byte)(0));
        _dex = ((byte)(0));
        _hp = ((uint)(0));
        _mp = ((uint)(0));
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte Str
    {
        get
        {
            return _str;
        }
        set
        {
            _str = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte Int
    {
        get
        {
            return _int;
        }
        set
        {
            _int = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte Wis
    {
        get
        {
            return _wis;
        }
        set
        {
            _wis = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte Con
    {
        get
        {
            return _con;
        }
        set
        {
            _con = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(byte), "0")]
    public byte Dex
    {
        get
        {
            return _dex;
        }
        set
        {
            _dex = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(uint), "0")]
    public uint Mp
    {
        get
        {
            return _mp;
        }
        set
        {
            _mp = value;
        }
    }
}
}
#pragma warning restore