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
public partial class MapSign : HybrasylEntity<MapSign>
{
    #region Private fields
    private string _name;
    private string _description;
    private string _message;
    private string _script;
    private BoardEffects _effect;
    private BoardType _type;
    private string _boardKey;
    private byte _x;
    private byte _y;
    #endregion
    
    [StringLengthAttribute(255, MinimumLength=1)]
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
    
    [StringLengthAttribute(65534, MinimumLength=1)]
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
    
    public BoardEffects Effect
    {
        get
        {
            return _effect;
        }
        set
        {
            _effect = value;
        }
    }
    
    [XmlAttribute]
    public BoardType Type
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
    
    [XmlAttribute]
    public string BoardKey
    {
        get
        {
            return _boardKey;
        }
        set
        {
            _boardKey = value;
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
}
}
#pragma warning restore
