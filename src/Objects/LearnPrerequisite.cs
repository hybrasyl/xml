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
public partial class LearnPrerequisite : HybrasylEntity<LearnPrerequisite>
{
    #region Private fields
    private byte _level;
    private byte _ab;
    private string _value;
    #endregion
    
    [XmlAttribute]
    public byte Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    
    [XmlAttribute]
    public byte Ab
    {
        get
        {
            return _ab;
        }
        set
        {
            _ab = value;
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
