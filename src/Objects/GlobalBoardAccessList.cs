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
public partial class GlobalBoardAccessList : HybrasylEntity<GlobalBoardAccessList>
{
    #region Private fields
    private List<string> _read;
    private List<string> _write;
    private List<string> _moderate;
    #endregion
    
    [XmlElement("Read", DataType="token")]
    [StringLengthAttribute(12, MinimumLength=4)]
    [RegularExpressionAttribute("[a-zA-Z]*")]
    public List<string> Read
    {
        get
        {
            return _read;
        }
        set
        {
            _read = value;
        }
    }
    
    [XmlElement("Write", DataType="token")]
    [StringLengthAttribute(12, MinimumLength=4)]
    [RegularExpressionAttribute("[a-zA-Z]*")]
    public List<string> Write
    {
        get
        {
            return _write;
        }
        set
        {
            _write = value;
        }
    }
    
    [XmlElement("Moderate", DataType="token")]
    [StringLengthAttribute(12, MinimumLength=4)]
    [RegularExpressionAttribute("[a-zA-Z]*")]
    public List<string> Moderate
    {
        get
        {
            return _moderate;
        }
        set
        {
            _moderate = value;
        }
    }
}
}
#pragma warning restore
