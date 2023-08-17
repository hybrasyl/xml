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
[XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Hybrasyl/2020-02", IsNullable=false)]
public partial class Npc : HybrasylEntity<Npc>
{
    #region Private fields
    private string _name;
    private string _displayName;
    private NpcAppearance _appearance;
    private List<LocalizedString> _strings;
    private List<NpcResponse> _responses;
    private NpcRoleList _roles;
    private bool _allowDead;
    private NpcInventory _inventory;
    private byte _x;
    private byte _y;
    #endregion
    
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
    
    public NpcAppearance Appearance
    {
        get
        {
            return _appearance;
        }
        set
        {
            _appearance = value;
        }
    }
    
    [XmlArrayItemAttribute("String", IsNullable=false)]
    public List<LocalizedString> Strings
    {
        get
        {
            return _strings;
        }
        set
        {
            _strings = value;
        }
    }
    
    [XmlArrayItemAttribute("Response", IsNullable=false)]
    public List<NpcResponse> Responses
    {
        get
        {
            return _responses;
        }
        set
        {
            _responses = value;
        }
    }
    
    public NpcRoleList Roles
    {
        get
        {
            return _roles;
        }
        set
        {
            _roles = value;
        }
    }
    
    public bool AllowDead
    {
        get
        {
            return _allowDead;
        }
        set
        {
            _allowDead = value;
        }
    }
    
    public NpcInventory Inventory
    {
        get
        {
            return _inventory;
        }
        set
        {
            _inventory = value;
        }
    }
    
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
}
}
#pragma warning restore
