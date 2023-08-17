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
public partial class CastModifierAdd : HybrasylEntity<CastModifierAdd>
{
    #region Private fields
    private int _match;
    private int _amount;
    private int _min;
    private int _max;
    #endregion
    
    public CastModifierAdd()
    {
        _match = -1;
        _min = -1;
        _max = 255;
    }
    
    [XmlAttribute]
    [DefaultValue(-1)]
    public int Match
    {
        get
        {
            return _match;
        }
        set
        {
            _match = value;
        }
    }
    
    [XmlAttribute]
    public int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(-1)]
    public int Min
    {
        get
        {
            return _min;
        }
        set
        {
            _min = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(255)]
    public int Max
    {
        get
        {
            return _max;
        }
        set
        {
            _max = value;
        }
    }
}
}
#pragma warning restore
