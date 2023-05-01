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
public partial class Variants : HybrasylEntity<Variants>
{
    #region Private fields
    private List<string> _name;
    private List<string> _group;
    #endregion
    
    [XmlElement("Name")]
    [StringLengthAttribute(255, MinimumLength=1)]
    public List<string> Name
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
    
    [XmlElement("Group")]
    [StringLengthAttribute(255, MinimumLength=1)]
    public List<string> Group
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
}
}
#pragma warning restore
