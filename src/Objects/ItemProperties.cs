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
public partial class ItemProperties : HybrasylEntity<ItemProperties>
{
    #region Private fields
    private Appearance _appearance;
    private List<CastModifier> _castModifiers;
    private Stackable _stackable;
    private Physical _physical;
    private List<Category> _categories;
    private Equipment _equipment;
    private StatModifiers _statModifiers;
    private ItemFlags _flags;
    private Variants _variants;
    private Vendor _vendor;
    private ItemDamage _damage;
    private Use _use;
    private ItemRestrictions _restrictions;
    private List<ItemMotion> _motions;
    private List<Proc> _procs;
    #endregion
    
    public ItemProperties()
    {
        _physical = new Physical();
        _stackable = new Stackable();
        _appearance = new Appearance();
    }
    
    public Appearance Appearance
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
    
    [XmlArrayItemAttribute("Match", IsNullable=false)]
    public List<CastModifier> CastModifiers
    {
        get
        {
            return _castModifiers;
        }
        set
        {
            _castModifiers = value;
        }
    }
    
    public Stackable Stackable
    {
        get
        {
            return _stackable;
        }
        set
        {
            _stackable = value;
        }
    }
    
    public Physical Physical
    {
        get
        {
            return _physical;
        }
        set
        {
            _physical = value;
        }
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<Category> Categories
    {
        get
        {
            return _categories;
        }
        set
        {
            _categories = value;
        }
    }
    
    public Equipment Equipment
    {
        get
        {
            return _equipment;
        }
        set
        {
            _equipment = value;
        }
    }
    
    public StatModifiers StatModifiers
    {
        get
        {
            return _statModifiers;
        }
        set
        {
            _statModifiers = value;
        }
    }
    
    public ItemFlags Flags
    {
        get
        {
            return _flags;
        }
        set
        {
            _flags = value;
        }
    }
    
    public Variants Variants
    {
        get
        {
            return _variants;
        }
        set
        {
            _variants = value;
        }
    }
    
    public Vendor Vendor
    {
        get
        {
            return _vendor;
        }
        set
        {
            _vendor = value;
        }
    }
    
    public ItemDamage Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    
    public Use Use
    {
        get
        {
            return _use;
        }
        set
        {
            _use = value;
        }
    }
    
    public ItemRestrictions Restrictions
    {
        get
        {
            return _restrictions;
        }
        set
        {
            _restrictions = value;
        }
    }
    
    [XmlArrayItemAttribute("Motion", IsNullable=false)]
    public List<ItemMotion> Motions
    {
        get
        {
            return _motions;
        }
        set
        {
            _motions = value;
        }
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<Proc> Procs
    {
        get
        {
            return _procs;
        }
        set
        {
            _procs = value;
        }
    }
}
}
#pragma warning restore
