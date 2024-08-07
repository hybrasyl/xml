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
public partial class Proc : HybrasylEntity<Proc>
{
    #region Private fields
    private ProcEventType _type;
    private string _castable;
    private string _script;
    private float _chance;
    #endregion
    
    [XmlAttribute]
    public ProcEventType Type
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
    public string Castable
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
    public float Chance
    {
        get
        {
            return _chance;
        }
        set
        {
            _chance = value;
        }
    }
}
}
#pragma warning restore
