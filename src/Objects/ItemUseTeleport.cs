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
public partial class ItemUseTeleport : HybrasylXmlEntity<ItemUseTeleport>
{
    #region Private fields
    private byte _x;
    private byte _y;
    private string _value;
    #endregion
    
    [XmlAttribute]
    public byte X
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }
    
    [XmlAttribute]
    public byte Y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
        }
    }
    
    [XmlTextAttribute]
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }
}
}
#pragma warning restore
