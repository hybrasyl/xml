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

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9037.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class Statuses : HybrasylEntity<Statuses>
{
    #region Private fields
    private List<AddStatus> _add;
    private List<string> _remove;
    #endregion
    
    public Statuses()
    {
        _remove = new List<string>();
        _add = new List<AddStatus>();
    }
    
    [XmlElement("Add")]
    public List<AddStatus> Add
    {
        get
        {
            return _add;
        }
        set
        {
            _add = value;
        }
    }
    
    [XmlElement("Remove")]
    [StringLengthAttribute(255, MinimumLength=1)]
    public List<string> Remove
    {
        get
        {
            return _remove;
        }
        set
        {
            _remove = value;
        }
    }
}
}
#pragma warning restore
