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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class Death : HybrasylEntity<Death>
{
    #region Private fields
    private DeathMap _map;
    private DeathComa _coma;
    private DeathPenalty _penalty;
    private DeathLegendMark _legendMark;
    private bool _active;
    private bool _perishable;
    private bool _groupNotify;
    #endregion
    
    public Death()
    {
        _penalty = new DeathPenalty();
        _coma = new DeathComa();
        _map = new DeathMap();
        _active = true;
        _perishable = true;
        _groupNotify = true;
    }
    
    public DeathMap Map
    {
        get
        {
            return _map;
        }
        set
        {
            _map = value;
        }
    }
    
    public DeathComa Coma
    {
        get
        {
            return _coma;
        }
        set
        {
            _coma = value;
        }
    }
    
    public DeathPenalty Penalty
    {
        get
        {
            return _penalty;
        }
        set
        {
            _penalty = value;
        }
    }
    
    public DeathLegendMark LegendMark
    {
        get
        {
            return _legendMark;
        }
        set
        {
            _legendMark = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool Perishable
    {
        get
        {
            return _perishable;
        }
        set
        {
            _perishable = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool GroupNotify
    {
        get
        {
            return _groupNotify;
        }
        set
        {
            _groupNotify = value;
        }
    }
}
}
#pragma warning restore
