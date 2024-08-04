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
public partial class LearnPrerequisites : HybrasylEntity<LearnPrerequisites>
{
    #region Private fields
    private List<LearnPrerequisite> _prerequisite;
    private string _forbidCookie;
    private string _requireCookie;
    private string _forbidMessage;
    private string _requireMessage;
    #endregion
    
    public LearnPrerequisites()
    {
        _prerequisite = new List<LearnPrerequisite>();
    }
    
    [XmlElement("Prerequisite")]
    public List<LearnPrerequisite> Prerequisite
    {
        get
        {
            return _prerequisite;
        }
        set
        {
            _prerequisite = value;
        }
    }
    
    [XmlAttribute]
    public string ForbidCookie
    {
        get
        {
            return _forbidCookie;
        }
        set
        {
            _forbidCookie = value;
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
    public string ForbidMessage
    {
        get
        {
            return _forbidMessage;
        }
        set
        {
            _forbidMessage = value;
        }
    }
    
    [XmlAttribute]
    public string RequireMessage
    {
        get
        {
            return _requireMessage;
        }
        set
        {
            _requireMessage = value;
        }
    }
}
}
#pragma warning restore
