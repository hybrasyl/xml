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
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class Item : HybrasylEntity<Item>
{
    #region Private fields
    private string _name;
    private string _unidentifiedName;
    private string _comment;
    private ItemProperties _properties;
    private bool _includeInMetafile;
    #endregion
    
    public Item()
    {
        _properties = new ItemProperties();
        _includeInMetafile = true;
    }
    
    [StringLengthAttribute(255, MinimumLength=1)]
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
    
    [StringLengthAttribute(255, MinimumLength=1)]
    public string UnidentifiedName
    {
        get
        {
            return _unidentifiedName;
        }
        set
        {
            _unidentifiedName = value;
        }
    }
    
    [StringLengthAttribute(65534, MinimumLength=1)]
    public string Comment
    {
        get
        {
            return _comment;
        }
        set
        {
            _comment = value;
        }
    }
    
    public ItemProperties Properties
    {
        get
        {
            return _properties;
        }
        set
        {
            _properties = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(true)]
    public bool IncludeInMetafile
    {
        get
        {
            return _includeInMetafile;
        }
        set
        {
            _includeInMetafile = value;
        }
    }
}
}
#pragma warning restore
