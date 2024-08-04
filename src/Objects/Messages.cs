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
public partial class Messages : HybrasylEntity<Messages>
{
    #region Private fields
    private string _target;
    private string _source;
    private string _group;
    private string _say;
    private string _shout;
    #endregion
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Source
    {
        get
        {
            return _source;
        }
        set
        {
            _source = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Group
    {
        get
        {
            return _group;
        }
        set
        {
            _group = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Say
    {
        get
        {
            return _say;
        }
        set
        {
            _say = value;
        }
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string Shout
    {
        get
        {
            return _shout;
        }
        set
        {
            _shout = value;
        }
    }
}
}
#pragma warning restore
