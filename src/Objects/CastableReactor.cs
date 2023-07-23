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
public partial class CastableReactor : HybrasylEntity<CastableReactor>
{
    #region Private fields
    private int _relativeX;
    private int _relativeY;
    private ushort _sprite;
    private string _script;
    private int _expiration;
    private string _uses;
    private bool _blocking;
    #endregion
    
    public CastableReactor()
    {
        _relativeX = 0;
        _relativeY = 0;
        _sprite = ((ushort)(0));
        _script = "0";
        _expiration = 0;
        _uses = "1";
        _blocking = false;
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int RelativeX
    {
        get
        {
            return _relativeX;
        }
        set
        {
            _relativeX = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int RelativeY
    {
        get
        {
            return _relativeY;
        }
        set
        {
            _relativeY = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(ushort), "0")]
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
    [DefaultValue("0")]
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
    [DefaultValue(0)]
    public int Expiration
    {
        get
        {
            return _expiration;
        }
        set
        {
            _expiration = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("1")]
    public string Uses
    {
        get
        {
            return _uses;
        }
        set
        {
            _uses = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool Blocking
    {
        get
        {
            return _blocking;
        }
        set
        {
            _blocking = value;
        }
    }
}
}
#pragma warning restore
