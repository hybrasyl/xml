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
public partial class HybrasylAge : HybrasylEntity<HybrasylAge>
{
    #region Private fields
    private string _name;
    private System.DateTime _startDate;
    private System.DateTime _endDate;
    private int _startYear;
    #endregion
    
    public HybrasylAge()
    {
        _startYear = 1;
    }
    
    [XmlAttribute]
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
    
    [XmlAttribute]
    public System.DateTime StartDate
    {
        get
        {
            return _startDate;
        }
        set
        {
            _startDate = value;
        }
    }
    
    [XmlAttribute]
    public System.DateTime EndDate
    {
        get
        {
            return _endDate;
        }
        set
        {
            _endDate = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(1)]
    public int StartYear
    {
        get
        {
            return _startYear;
        }
        set
        {
            _startYear = value;
        }
    }
}
}
#pragma warning restore
