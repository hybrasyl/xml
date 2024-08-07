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
public partial class Chat : HybrasylEntity<Chat>
{
    #region Private fields
    private bool _commandsEnabled;
    private string _commandPrefix;
    #endregion
    
    public Chat()
    {
        _commandsEnabled = true;
        _commandPrefix = "/";
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool CommandsEnabled
    {
        get
        {
            return _commandsEnabled;
        }
        set
        {
            _commandsEnabled = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue("/")]
    public string CommandPrefix
    {
        get
        {
            return _commandPrefix;
        }
        set
        {
            _commandPrefix = value;
        }
    }
}
}
#pragma warning restore
