// This file is part of Project Hybrasyl.
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
// 
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
// 
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
// 
// (C) 2020-2023 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class StatModifiers
{
    [XmlIgnore]
    public List<ElementalModifier> Augments =>
        ElementalModifiers != null
            ? ElementalModifiers.Where(predicate: x => x.Type == ElementalModifierType.Augment).ToList()
            : new List<ElementalModifier>();

    [XmlIgnore]
    public List<ElementalModifier> Resistances =>
        ElementalModifiers != null
            ? ElementalModifiers.Where(predicate: x => x.Type == ElementalModifierType.Resistance).ToList()
            : new List<ElementalModifier>();

    public string BonusString
    {
        get
        {
            var defaultDesc = "";

            defaultDesc += FormatBonusNum(BonusHp, "Hp");
            defaultDesc += FormatBonusNum(BonusMp, "Mp");
            defaultDesc += FormatBonusNum(BonusStr, "Str");
            defaultDesc += FormatBonusNum(BonusInt, "Int");
            defaultDesc += FormatBonusNum(BonusWis, "Wis");
            defaultDesc += FormatBonusNum(BonusCon, "Con");
            defaultDesc += FormatBonusNum(BonusDex, "Dex");
            defaultDesc += FormatBonusPct(BonusCrit, "Crit");
            defaultDesc += FormatBonusPct(BonusMagicCrit, "Magic Crit");
            defaultDesc += FormatBonusPct(BonusDmg, "Dmg");
            defaultDesc += FormatBonusPct(BonusHit, "Hit");
            defaultDesc += FormatBonusNum(BonusAc, "Ac");
            defaultDesc += FormatBonusPct(BonusMr, "Mr");
            defaultDesc += FormatBonusPct(BonusRegen, "Regen");
            defaultDesc += FormatBonusPct(BonusReflectMagical, "Reflect Magic");
            defaultDesc += FormatBonusPct(BonusReflectPhysical, "Reflect Phys");
            defaultDesc += FormatBonusPct(BonusExtraGold, "Gold");
            defaultDesc += FormatBonusPct(BonusDodge, "Dodge");
            defaultDesc += FormatBonusPct(BonusMagicDodge, "Magic Dodge");
            defaultDesc += FormatBonusPct(BonusExtraXp, "Xp");
            defaultDesc += FormatBonusPct(BonusExtraItemFind, "Items");
            defaultDesc += FormatBonusPct(BonusLifeSteal, "Life Steal");
            defaultDesc += FormatBonusPct(BonusManaSteal, "Mana Steal");
            defaultDesc += FormatBonusNum(Shield, "Shield");
            return defaultDesc;
        }
    }


    public double GetElementalAugment(string element) => GetElementalModifier(ElementalModifierType.Augment, element);

    public double GetElementalResistance(string element) =>
        GetElementalModifier(ElementalModifierType.Resistance, element);

    // The server adds every matching entry to a total that starts at 0 (ElementalModifiers.Apply),
    // and uses the total as a fraction: 0.03 is 3%. These helpers give the same value.
    private double GetElementalModifier(ElementalModifierType type, string element) =>
        ElementalModifiers?.Where(predicate: x => x.Type == type && ElementTable.KeyComparer.Equals(x.Element, element))
            .Sum(selector: x => x.Modifier) ?? 0.0;

    public static string FormatBonusPct(string bonus, string name, float scale = 1)
    {
        if (string.IsNullOrEmpty(bonus)) return string.Empty;
        if (!double.TryParse(bonus, out var num)) return $"??? {name} \n";
        num /= scale;
        num = num * 100;
        return num == 0 ? string.Empty : $"{(num > 0 ? "+" + num + "%" : num + "%")} {name}\n";
    }

    public static string FormatBonusNum(string bonus, string name)
    {
        if (string.IsNullOrEmpty(bonus)) return string.Empty;
        if (!long.TryParse(bonus, out var num)) return $"??? {name} \n";
        return num == 0 ? string.Empty : $"{(num > 0 ? "+" + num : num)} {name}\n";
    }

    public static string Combine(string sm1, string sm2)
    {
        if (string.IsNullOrEmpty(sm1) && string.IsNullOrEmpty(sm2)) return string.Empty;

        if (long.TryParse(sm1, out var sml1) && long.TryParse(sm2, out var sml2))
            return Convert.ToString(sml1 + sml2);
        if (double.TryParse(sm1, out var smd1) && double.TryParse(sm2, out var smd2))
            return Convert.ToString(smd1 + smd2);

        if (string.IsNullOrEmpty(sm2)) return sm1;
        if (string.IsNullOrEmpty(sm1)) return sm2;
        return $"{sm1} + {sm2}";
    }

    public static StatModifiers operator +(StatModifiers sm1, StatModifiers sm2)
    {
        // Return a new object. The operands do not change, and the result shares no list or entry with them.
        var ret = sm1.Clone<StatModifiers>(true);
        ret.CurrentHp = Combine(ret.CurrentHp, sm2.CurrentHp);
        ret.CurrentMp = Combine(ret.CurrentMp, sm2.CurrentMp);
        ret.BaseHp = Combine(ret.BaseHp, sm2.BaseHp);
        ret.BonusHp = Combine(ret.BonusHp, sm2.BonusHp);
        ret.BaseMp = Combine(ret.BaseMp, sm2.BaseMp);
        ret.BonusMp = Combine(ret.BonusMp, sm2.BonusMp);
        ret.BaseStr = Combine(ret.BaseStr, sm2.BaseStr);
        ret.BonusStr = Combine(ret.BonusStr, sm2.BonusStr);
        ret.BaseCon = Combine(ret.BaseCon, sm2.BaseCon);
        ret.BonusCon = Combine(ret.BonusCon, sm2.BonusCon);
        ret.BaseDex = Combine(ret.BaseDex, sm2.BaseDex);
        ret.BonusDex = Combine(ret.BonusDex, sm2.BonusDex);
        ret.BaseInt = Combine(ret.BaseInt, sm2.BaseInt);
        ret.BonusInt = Combine(ret.BonusInt, sm2.BonusInt);
        ret.BaseWis = Combine(ret.BaseWis, sm2.BaseWis);
        ret.BonusWis = Combine(ret.BonusWis, sm2.BonusWis);
        ret.BaseCrit = Combine(ret.BaseCrit, sm2.BaseCrit);
        ret.BonusCrit = Combine(ret.BonusCrit, sm2.BonusCrit);
        ret.BaseMagicCrit = Combine(ret.BaseMagicCrit, sm2.BaseMagicCrit);
        ret.BonusMagicCrit = Combine(ret.BonusMagicCrit, sm2.BonusMagicCrit);
        ret.BaseDmg = Combine(ret.BaseDmg, sm2.BaseDmg);
        ret.BonusDmg = Combine(ret.BonusDmg, sm2.BonusDmg);
        ret.BaseHit = Combine(ret.BaseHit, sm2.BaseHit);
        ret.BonusHit = Combine(ret.BonusHit, sm2.BonusHit);
        ret.BaseAc = Combine(ret.BaseAc, sm2.BaseAc);
        ret.BonusAc = Combine(ret.BonusAc, sm2.BonusAc);
        ret.BaseMr = Combine(ret.BaseMr, sm2.BaseMr);
        ret.BonusMr = Combine(ret.BonusMr, sm2.BonusMr);
        ret.BaseRegen = Combine(ret.BaseRegen, sm2.BaseRegen);
        ret.BonusRegen = Combine(ret.BonusRegen, sm2.BonusRegen);
        ret.BaseInboundDamageModifier = Combine(ret.BaseInboundDamageModifier, sm2.BaseInboundDamageModifier);
        ret.BonusInboundDamageModifier = Combine(ret.BonusInboundDamageModifier, sm2.BonusInboundDamageModifier);
        ret.BaseInboundHealModifier = Combine(ret.BaseInboundHealModifier, sm2.BaseInboundHealModifier);
        ret.BonusInboundHealModifier = Combine(ret.BonusInboundHealModifier, sm2.BonusInboundHealModifier);
        ret.BaseOutboundDamageModifier = Combine(ret.BaseOutboundDamageModifier, sm2.BaseOutboundDamageModifier);
        ret.BonusOutboundDamageModifier = Combine(ret.BonusOutboundDamageModifier, sm2.BonusOutboundDamageModifier);
        ret.BaseOutboundHealModifier = Combine(ret.BaseOutboundHealModifier, sm2.BaseOutboundHealModifier);
        ret.BonusOutboundHealModifier = Combine(ret.BonusOutboundHealModifier, sm2.BonusOutboundHealModifier);
        ret.BaseReflectMagical = Combine(ret.BaseReflectMagical, sm2.BaseReflectMagical);
        ret.BonusReflectMagical = Combine(ret.BonusReflectMagical, sm2.BonusReflectMagical);
        ret.BaseReflectPhysical = Combine(ret.BaseReflectPhysical, sm2.BaseReflectPhysical);
        ret.BonusReflectPhysical = Combine(ret.BonusReflectPhysical, sm2.BonusReflectPhysical);
        ret.BaseExtraGold = Combine(ret.BaseExtraGold, sm2.BaseExtraGold);
        ret.BonusExtraGold = Combine(ret.BonusExtraGold, sm2.BonusExtraGold);
        ret.BaseDodge = Combine(ret.BaseDodge, sm2.BaseDodge);
        ret.BonusDodge = Combine(ret.BonusDodge, sm2.BonusDodge);
        ret.BaseMagicDodge = Combine(ret.BaseMagicDodge, sm2.BaseMagicDodge);
        ret.BonusMagicDodge = Combine(ret.BonusMagicDodge, sm2.BonusMagicDodge);
        ret.BaseExtraXp = Combine(ret.BaseExtraXp, sm2.BaseExtraXp);
        ret.BonusExtraXp = Combine(ret.BonusExtraXp, sm2.BonusExtraXp);
        ret.BaseExtraItemFind = Combine(ret.BaseExtraItemFind, sm2.BaseExtraItemFind);
        ret.BonusExtraItemFind = Combine(ret.BonusExtraItemFind, sm2.BonusExtraItemFind);
        ret.BaseLifeSteal = Combine(ret.BaseLifeSteal, sm2.BaseLifeSteal);
        ret.BonusLifeSteal = Combine(ret.BonusLifeSteal, sm2.BonusLifeSteal);
        ret.BaseManaSteal = Combine(ret.BaseManaSteal, sm2.BaseManaSteal);
        ret.BonusManaSteal = Combine(ret.BonusManaSteal, sm2.BonusManaSteal);
        ret.BaseInboundDamageToMp = Combine(ret.BaseInboundDamageToMp, sm2.BaseInboundDamageToMp);
        ret.BonusInboundDamageToMp = Combine(ret.BonusInboundDamageToMp, sm2.BonusInboundDamageToMp);
        ret.Shield = Combine(ret.Shield, sm2.Shield);
        // Keep both lists. The server adds entries of the same type and element.
        if (sm2.ElementalModifiers is { Count: > 0 })
        {
            ret.ElementalModifiers ??= new List<ElementalModifier>();
            ret.ElementalModifiers.AddRange(sm2.Clone<StatModifiers>().ElementalModifiers);
        }

        return ret;
    }
}