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
public partial class BoardEffects : HybrasylEntity<BoardEffects>
{
    #region Private fields
    private ushort _onEntry;
    private short _onEntrySpeed;
    #endregion
    
    public BoardEffects()
    {
        _onEntrySpeed = ((short)(100));
    }
    
    [XmlAttribute]
    public ushort OnEntry
    {
        get
        {
            return _onEntry;
        }
        set
        {
            _onEntry = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(short), "100")]
    public short OnEntrySpeed
    {
        get
        {
            return _onEntrySpeed;
        }
        set
        {
            _onEntrySpeed = value;
        }
    }
}
}
#pragma warning restore
