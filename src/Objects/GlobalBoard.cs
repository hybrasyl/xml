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
public partial class GlobalBoard : HybrasylEntity<GlobalBoard>
{
    #region Private fields
    private GlobalBoardAccessList _accessList;
    private string _name;
    private string _displayName;
    #endregion
    
    public GlobalBoardAccessList AccessList
    {
        get
        {
            return _accessList;
        }
        set
        {
            _accessList = value;
        }
    }
    
    [XmlAttribute(DataType="token")]
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
    public string DisplayName
    {
        get
        {
            return _displayName;
        }
        set
        {
            _displayName = value;
        }
    }
}
}
#pragma warning restore
