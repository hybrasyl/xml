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
public partial class Reactor : HybrasylEntity<Reactor>
{
    #region Private fields
    private string _description;
    private string _script;
    private byte _x;
    private byte _y;
    private bool _blocking;
    private bool _allowDead;
    #endregion
    
    public Reactor()
    {
        _blocking = false;
        _allowDead = false;
    }
    
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
    public byte X
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }
    
    [XmlAttribute]
    public byte Y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
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
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool AllowDead
    {
        get
        {
            return _allowDead;
        }
        set
        {
            _allowDead = value;
        }
    }
}
}
#pragma warning restore