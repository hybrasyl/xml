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
public partial class Requirement : ClassRequirement
{
    #region Private fields
    private List<Class> _class;
    private string _forbidCookie;
    private string _requireCookie;
    #endregion
    
    public Requirement()
    {
        _class = new List<Class>();
    }
    
    [XmlAttribute]
    public List<Class> Class
    {
        get
        {
            return _class;
        }
        set
        {
            _class = value;
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
}
}
#pragma warning restore
