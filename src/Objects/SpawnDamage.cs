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
public partial class SpawnDamage : HybrasylEntity<SpawnDamage>
{
    #region Private fields
    private string _minDmg;
    private string _maxDmg;
    private List<ElementType> _elements;
    #endregion
    
    public SpawnDamage()
    {
        _elements = new List<ElementType>();
    }
    
    [XmlAttribute]
    public string MinDmg
    {
        get
        {
            return _minDmg;
        }
        set
        {
            _minDmg = value;
        }
    }
    
    [XmlAttribute]
    public string MaxDmg
    {
        get
        {
            return _maxDmg;
        }
        set
        {
            _maxDmg = value;
        }
    }
    
    [XmlAttribute]
    public List<ElementType> Elements
    {
        get
        {
            return _elements;
        }
        set
        {
            _elements = value;
        }
    }
}
}
#pragma warning restore
