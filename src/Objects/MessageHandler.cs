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
public partial class MessageHandler : HybrasylEntity<MessageHandler>
{
    #region Private fields
    private List<string> _target;
    private string _handler;
    private string _passthrough;
    #endregion
    
    public MessageHandler()
    {
        _passthrough = "false";
    }
    
    [XmlElement("Target", DataType="token")]
    public List<string> Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }
    
    [XmlAttribute(DataType="token")]
    public string Handler
    {
        get
        {
            return _handler;
        }
        set
        {
            _handler = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("false")]
    public string Passthrough
    {
        get
        {
            return _passthrough;
        }
        set
        {
            _passthrough = value;
        }
    }
}
}
#pragma warning restore
