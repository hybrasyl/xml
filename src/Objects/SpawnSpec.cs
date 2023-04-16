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
public partial class SpawnSpec : HybrasylXmlEntity<SpawnSpec>
{
    #region Private fields
    private string _minCount;
    private string _maxCount;
    private string _maxPerInterval;
    private string _interval;
    private string _limit;
    private string _when;
    private string _percentage;
    private bool _disabled;
    #endregion
    
    public SpawnSpec()
    {
        _disabled = false;
    }
    
    [XmlAttribute]
    public string MinCount
    {
        get
        {
            return _minCount;
        }
        set
        {
            _minCount = value;
        }
    }
    
    [XmlAttribute]
    public string MaxCount
    {
        get
        {
            return _maxCount;
        }
        set
        {
            _maxCount = value;
        }
    }
    
    [XmlAttribute]
    public string MaxPerInterval
    {
        get
        {
            return _maxPerInterval;
        }
        set
        {
            _maxPerInterval = value;
        }
    }
    
    [XmlAttribute]
    public string Interval
    {
        get
        {
            return _interval;
        }
        set
        {
            _interval = value;
        }
    }
    
    [XmlAttribute]
    public string Limit
    {
        get
        {
            return _limit;
        }
        set
        {
            _limit = value;
        }
    }
    
    [XmlAttribute]
    public string When
    {
        get
        {
            return _when;
        }
        set
        {
            _when = value;
        }
    }
    
    [XmlAttribute]
    public string Percentage
    {
        get
        {
            return _percentage;
        }
        set
        {
            _percentage = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(false)]
    public bool Disabled
    {
        get
        {
            return _disabled;
        }
        set
        {
            _disabled = value;
        }
    }
}
}
#pragma warning restore
