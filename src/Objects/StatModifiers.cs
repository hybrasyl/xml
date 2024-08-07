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
public partial class StatModifiers : HybrasylEntity<StatModifiers>
{
    #region Private fields
    private List<ElementalModifier> _elementalModifiers;
    private string _baseStr;
    private string _baseInt;
    private string _baseWis;
    private string _baseCon;
    private string _baseDex;
    private string _baseHp;
    private string _baseMp;
    private string _currentHp;
    private string _currentMp;
    private string _currentGold;
    private string _currentXp;
    private string _currentFaith;
    private string _baseHit;
    private string _baseDmg;
    private string _baseAc;
    private string _baseRegen;
    private string _baseMr;
    private string _baseCrit;
    private string _baseMagicCrit;
    private string _baseInboundDamageToMp;
    private ElementType _baseOffensiveElement;
    private ElementType _baseDefensiveElement;
    private string _baseExtraFaith;
    private ElementType _offensiveElementOverride;
    private ElementType _defensiveElementOverride;
    private string _baseInboundDamageModifier;
    private string _baseOutboundDamageModifier;
    private string _baseInboundHealModifier;
    private string _baseOutboundHealModifier;
    private string _damageType;
    private string _baseReflectMagical;
    private string _baseReflectPhysical;
    private string _baseExtraGold;
    private string _baseDodge;
    private string _baseMagicDodge;
    private string _baseExtraXp;
    private string _baseExtraItemFind;
    private string _baseLifeSteal;
    private string _baseManaSteal;
    private string _bonusStr;
    private string _bonusInt;
    private string _bonusWis;
    private string _bonusCon;
    private string _bonusDex;
    private string _bonusHp;
    private string _bonusMp;
    private string _bonusHit;
    private string _bonusDmg;
    private string _bonusAc;
    private string _bonusRegen;
    private string _bonusMr;
    private string _bonusCrit;
    private string _bonusMagicCrit;
    private string _bonusInboundDamageModifier;
    private string _bonusOutboundDamageModifier;
    private string _bonusInboundHealModifier;
    private string _bonusOutboundHealModifier;
    private string _bonusReflectMagical;
    private string _bonusReflectPhysical;
    private string _bonusExtraGold;
    private string _bonusDodge;
    private string _bonusMagicDodge;
    private string _bonusExtraXp;
    private string _bonusExtraItemFind;
    private string _bonusLifeSteal;
    private string _bonusManaSteal;
    private string _bonusInboundDamageToMp;
    private string _bonusExtraFaith;
    private string _shield;
    #endregion
    
    public StatModifiers()
    {
        _baseOffensiveElement = ElementType.None;
        _baseDefensiveElement = ElementType.None;
        _offensiveElementOverride = ElementType.None;
        _defensiveElementOverride = ElementType.None;
    }
    
    [XmlArrayItemAttribute(IsNullable=false)]
    public List<ElementalModifier> ElementalModifiers
    {
        get
        {
            return _elementalModifiers;
        }
        set
        {
            _elementalModifiers = value;
        }
    }
    
    [XmlAttribute]
    public string BaseStr
    {
        get
        {
            return _baseStr;
        }
        set
        {
            _baseStr = value;
        }
    }
    
    [XmlAttribute]
    public string BaseInt
    {
        get
        {
            return _baseInt;
        }
        set
        {
            _baseInt = value;
        }
    }
    
    [XmlAttribute]
    public string BaseWis
    {
        get
        {
            return _baseWis;
        }
        set
        {
            _baseWis = value;
        }
    }
    
    [XmlAttribute]
    public string BaseCon
    {
        get
        {
            return _baseCon;
        }
        set
        {
            _baseCon = value;
        }
    }
    
    [XmlAttribute]
    public string BaseDex
    {
        get
        {
            return _baseDex;
        }
        set
        {
            _baseDex = value;
        }
    }
    
    [XmlAttribute]
    public string BaseHp
    {
        get
        {
            return _baseHp;
        }
        set
        {
            _baseHp = value;
        }
    }
    
    [XmlAttribute]
    public string BaseMp
    {
        get
        {
            return _baseMp;
        }
        set
        {
            _baseMp = value;
        }
    }
    
    [XmlAttribute]
    public string CurrentHp
    {
        get
        {
            return _currentHp;
        }
        set
        {
            _currentHp = value;
        }
    }
    
    [XmlAttribute]
    public string CurrentMp
    {
        get
        {
            return _currentMp;
        }
        set
        {
            _currentMp = value;
        }
    }
    
    [XmlAttribute]
    public string CurrentGold
    {
        get
        {
            return _currentGold;
        }
        set
        {
            _currentGold = value;
        }
    }
    
    [XmlAttribute]
    public string CurrentXp
    {
        get
        {
            return _currentXp;
        }
        set
        {
            _currentXp = value;
        }
    }
    
    [XmlAttribute]
    public string CurrentFaith
    {
        get
        {
            return _currentFaith;
        }
        set
        {
            _currentFaith = value;
        }
    }
    
    [XmlAttribute]
    public string BaseHit
    {
        get
        {
            return _baseHit;
        }
        set
        {
            _baseHit = value;
        }
    }
    
    [XmlAttribute]
    public string BaseDmg
    {
        get
        {
            return _baseDmg;
        }
        set
        {
            _baseDmg = value;
        }
    }
    
    [XmlAttribute]
    public string BaseAc
    {
        get
        {
            return _baseAc;
        }
        set
        {
            _baseAc = value;
        }
    }
    
    [XmlAttribute]
    public string BaseRegen
    {
        get
        {
            return _baseRegen;
        }
        set
        {
            _baseRegen = value;
        }
    }
    
    [XmlAttribute]
    public string BaseMr
    {
        get
        {
            return _baseMr;
        }
        set
        {
            _baseMr = value;
        }
    }
    
    [XmlAttribute]
    public string BaseCrit
    {
        get
        {
            return _baseCrit;
        }
        set
        {
            _baseCrit = value;
        }
    }
    
    [XmlAttribute]
    public string BaseMagicCrit
    {
        get
        {
            return _baseMagicCrit;
        }
        set
        {
            _baseMagicCrit = value;
        }
    }
    
    [XmlAttribute]
    public string BaseInboundDamageToMp
    {
        get
        {
            return _baseInboundDamageToMp;
        }
        set
        {
            _baseInboundDamageToMp = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(ElementType.None)]
    public ElementType BaseOffensiveElement
    {
        get
        {
            return _baseOffensiveElement;
        }
        set
        {
            _baseOffensiveElement = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(ElementType.None)]
    public ElementType BaseDefensiveElement
    {
        get
        {
            return _baseDefensiveElement;
        }
        set
        {
            _baseDefensiveElement = value;
        }
    }
    
    [XmlAttribute]
    public string BaseExtraFaith
    {
        get
        {
            return _baseExtraFaith;
        }
        set
        {
            _baseExtraFaith = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(ElementType.None)]
    public ElementType OffensiveElementOverride
    {
        get
        {
            return _offensiveElementOverride;
        }
        set
        {
            _offensiveElementOverride = value;
        }
    }
    
    [XmlAttribute]
    [DefaultValue(ElementType.None)]
    public ElementType DefensiveElementOverride
    {
        get
        {
            return _defensiveElementOverride;
        }
        set
        {
            _defensiveElementOverride = value;
        }
    }
    
    [XmlAttribute]
    public string BaseInboundDamageModifier
    {
        get
        {
            return _baseInboundDamageModifier;
        }
        set
        {
            _baseInboundDamageModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BaseOutboundDamageModifier
    {
        get
        {
            return _baseOutboundDamageModifier;
        }
        set
        {
            _baseOutboundDamageModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BaseInboundHealModifier
    {
        get
        {
            return _baseInboundHealModifier;
        }
        set
        {
            _baseInboundHealModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BaseOutboundHealModifier
    {
        get
        {
            return _baseOutboundHealModifier;
        }
        set
        {
            _baseOutboundHealModifier = value;
        }
    }
    
    [XmlAttribute]
    public string DamageType
    {
        get
        {
            return _damageType;
        }
        set
        {
            _damageType = value;
        }
    }
    
    [XmlAttribute]
    public string BaseReflectMagical
    {
        get
        {
            return _baseReflectMagical;
        }
        set
        {
            _baseReflectMagical = value;
        }
    }
    
    [XmlAttribute]
    public string BaseReflectPhysical
    {
        get
        {
            return _baseReflectPhysical;
        }
        set
        {
            _baseReflectPhysical = value;
        }
    }
    
    [XmlAttribute]
    public string BaseExtraGold
    {
        get
        {
            return _baseExtraGold;
        }
        set
        {
            _baseExtraGold = value;
        }
    }
    
    [XmlAttribute]
    public string BaseDodge
    {
        get
        {
            return _baseDodge;
        }
        set
        {
            _baseDodge = value;
        }
    }
    
    [XmlAttribute]
    public string BaseMagicDodge
    {
        get
        {
            return _baseMagicDodge;
        }
        set
        {
            _baseMagicDodge = value;
        }
    }
    
    [XmlAttribute]
    public string BaseExtraXp
    {
        get
        {
            return _baseExtraXp;
        }
        set
        {
            _baseExtraXp = value;
        }
    }
    
    [XmlAttribute]
    public string BaseExtraItemFind
    {
        get
        {
            return _baseExtraItemFind;
        }
        set
        {
            _baseExtraItemFind = value;
        }
    }
    
    [XmlAttribute]
    public string BaseLifeSteal
    {
        get
        {
            return _baseLifeSteal;
        }
        set
        {
            _baseLifeSteal = value;
        }
    }
    
    [XmlAttribute]
    public string BaseManaSteal
    {
        get
        {
            return _baseManaSteal;
        }
        set
        {
            _baseManaSteal = value;
        }
    }
    
    [XmlAttribute]
    public string BonusStr
    {
        get
        {
            return _bonusStr;
        }
        set
        {
            _bonusStr = value;
        }
    }
    
    [XmlAttribute]
    public string BonusInt
    {
        get
        {
            return _bonusInt;
        }
        set
        {
            _bonusInt = value;
        }
    }
    
    [XmlAttribute]
    public string BonusWis
    {
        get
        {
            return _bonusWis;
        }
        set
        {
            _bonusWis = value;
        }
    }
    
    [XmlAttribute]
    public string BonusCon
    {
        get
        {
            return _bonusCon;
        }
        set
        {
            _bonusCon = value;
        }
    }
    
    [XmlAttribute]
    public string BonusDex
    {
        get
        {
            return _bonusDex;
        }
        set
        {
            _bonusDex = value;
        }
    }
    
    [XmlAttribute]
    public string BonusHp
    {
        get
        {
            return _bonusHp;
        }
        set
        {
            _bonusHp = value;
        }
    }
    
    [XmlAttribute]
    public string BonusMp
    {
        get
        {
            return _bonusMp;
        }
        set
        {
            _bonusMp = value;
        }
    }
    
    [XmlAttribute]
    public string BonusHit
    {
        get
        {
            return _bonusHit;
        }
        set
        {
            _bonusHit = value;
        }
    }
    
    [XmlAttribute]
    public string BonusDmg
    {
        get
        {
            return _bonusDmg;
        }
        set
        {
            _bonusDmg = value;
        }
    }
    
    [XmlAttribute]
    public string BonusAc
    {
        get
        {
            return _bonusAc;
        }
        set
        {
            _bonusAc = value;
        }
    }
    
    [XmlAttribute]
    public string BonusRegen
    {
        get
        {
            return _bonusRegen;
        }
        set
        {
            _bonusRegen = value;
        }
    }
    
    [XmlAttribute]
    public string BonusMr
    {
        get
        {
            return _bonusMr;
        }
        set
        {
            _bonusMr = value;
        }
    }
    
    [XmlAttribute]
    public string BonusCrit
    {
        get
        {
            return _bonusCrit;
        }
        set
        {
            _bonusCrit = value;
        }
    }
    
    [XmlAttribute]
    public string BonusMagicCrit
    {
        get
        {
            return _bonusMagicCrit;
        }
        set
        {
            _bonusMagicCrit = value;
        }
    }
    
    [XmlAttribute]
    public string BonusInboundDamageModifier
    {
        get
        {
            return _bonusInboundDamageModifier;
        }
        set
        {
            _bonusInboundDamageModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BonusOutboundDamageModifier
    {
        get
        {
            return _bonusOutboundDamageModifier;
        }
        set
        {
            _bonusOutboundDamageModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BonusInboundHealModifier
    {
        get
        {
            return _bonusInboundHealModifier;
        }
        set
        {
            _bonusInboundHealModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BonusOutboundHealModifier
    {
        get
        {
            return _bonusOutboundHealModifier;
        }
        set
        {
            _bonusOutboundHealModifier = value;
        }
    }
    
    [XmlAttribute]
    public string BonusReflectMagical
    {
        get
        {
            return _bonusReflectMagical;
        }
        set
        {
            _bonusReflectMagical = value;
        }
    }
    
    [XmlAttribute]
    public string BonusReflectPhysical
    {
        get
        {
            return _bonusReflectPhysical;
        }
        set
        {
            _bonusReflectPhysical = value;
        }
    }
    
    [XmlAttribute]
    public string BonusExtraGold
    {
        get
        {
            return _bonusExtraGold;
        }
        set
        {
            _bonusExtraGold = value;
        }
    }
    
    [XmlAttribute]
    public string BonusDodge
    {
        get
        {
            return _bonusDodge;
        }
        set
        {
            _bonusDodge = value;
        }
    }
    
    [XmlAttribute]
    public string BonusMagicDodge
    {
        get
        {
            return _bonusMagicDodge;
        }
        set
        {
            _bonusMagicDodge = value;
        }
    }
    
    [XmlAttribute]
    public string BonusExtraXp
    {
        get
        {
            return _bonusExtraXp;
        }
        set
        {
            _bonusExtraXp = value;
        }
    }
    
    [XmlAttribute]
    public string BonusExtraItemFind
    {
        get
        {
            return _bonusExtraItemFind;
        }
        set
        {
            _bonusExtraItemFind = value;
        }
    }
    
    [XmlAttribute]
    public string BonusLifeSteal
    {
        get
        {
            return _bonusLifeSteal;
        }
        set
        {
            _bonusLifeSteal = value;
        }
    }
    
    [XmlAttribute]
    public string BonusManaSteal
    {
        get
        {
            return _bonusManaSteal;
        }
        set
        {
            _bonusManaSteal = value;
        }
    }
    
    [XmlAttribute]
    public string BonusInboundDamageToMp
    {
        get
        {
            return _bonusInboundDamageToMp;
        }
        set
        {
            _bonusInboundDamageToMp = value;
        }
    }
    
    [XmlAttribute]
    public string BonusExtraFaith
    {
        get
        {
            return _bonusExtraFaith;
        }
        set
        {
            _bonusExtraFaith = value;
        }
    }
    
    [XmlAttribute]
    public string Shield
    {
        get
        {
            return _shield;
        }
        set
        {
            _shield = value;
        }
    }
}
}
#pragma warning restore
