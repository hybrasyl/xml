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
public partial class Animation : HybrasylEntity<Animation>
{
    #region Private fields
    private ushort _id;
    private short _speed;
    #endregion
    
    public Animation()
    {
        _speed = ((short)(100));
    }
    
    [XmlAttribute]
    public ushort Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(typeof(short), "100")]
    public short Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }
}
}
#pragma warning restore
