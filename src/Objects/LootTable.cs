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
public partial class LootTable : HybrasylXmlEntity<LootTable>
{
    #region Private fields
    private List<LootTableItemList> _items;
    private LootTableGold _gold;
    private LootTableXp _xp;
    private int _rolls;
    private double _chance;
    #endregion
    
    public LootTable()
    {
        _rolls = 0;
        _chance = 0D;
    }
    
    [XmlElement("Items")]
    public List<LootTableItemList> Items
    {
        get
        {
            return _items;
        }
        set
        {
            _items = value;
        }
    }
    
    public LootTableGold Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
        }
    }
    
    public LootTableXp Xp
    {
        get
        {
            return _xp;
        }
        set
        {
            _xp = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0)]
    public int Rolls
    {
        get
        {
            return _rolls;
        }
        set
        {
            _rolls = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(0D)]
    public double Chance
    {
        get
        {
            return _chance;
        }
        set
        {
            _chance = value;
        }
    }
}
}
#pragma warning restore
