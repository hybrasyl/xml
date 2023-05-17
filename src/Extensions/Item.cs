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
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Item : ICategorizable, ILoadOnStart<Item>, IPostProcessable<Item>
{
    [XmlIgnore] public static SHA256 sha = SHA256.Create();

    [XmlIgnore] public bool IsVariant { get; set; }
    [XmlIgnore] public Guid ParentGuid { get; set; }
    [XmlIgnore] public Guid Variant { get; set; }

    [XmlIgnore]
    [Obsolete("Use ParentGuid instead")]
    public Item ParentItem { get; set; }

    [XmlIgnore] public Dictionary<string, List<Item>> Variants { get; set; }

    public IEnumerable<SlotRestriction> SlotRequirements =>
        (Properties.Restrictions?.SlotRestrictions ?? new List<SlotRestriction>()).Where(predicate: x =>
            x.Type == SlotRestrictionType.ItemRequired);

    public IEnumerable<SlotRestriction> SlotProhibits =>
        (Properties.Restrictions?.SlotRestrictions ?? new List<SlotRestriction>()).Where(predicate: x =>
            x.Type == SlotRestrictionType.ItemProhibited);

    public string Id => GenerateId(Name, Gender);
    public int IdInt => int.Parse(Id, NumberStyles.HexNumber);

    [XmlIgnore] public override string PrimaryKey => Id;
    [XmlIgnore] public override List<string> SecondaryKeys => new() { Name, Name.ToLower() };

    [XmlIgnore]
    public List<string> CategoryList
    {
        get
        {
            if (Properties?.Categories is not null)
                return Properties.Categories.Select(selector: x => x.Value.ToLower()).ToList();
            return new List<string>();
        }
    }

    [XmlIgnore]
    public List<Category> Categories
    {
        get
        {
            Properties ??= new ItemProperties();
            Properties.Categories ??= new List<Category>();
            return Properties.Categories;
        }
        set
        {
            Properties ??= new ItemProperties();
            Properties.Categories ??= new List<Category>();
            Properties.Categories = value;
        }
    }

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Item>.LoadAll(manager, path);

    public static void ProcessAll(IWorldDataManager manager)
    {
        var ret = new XmlProcessResult();
        foreach (var item in manager
                     .Find<Item>(condition: x => x.Properties.Variants != null && x.Properties.Variants.Group.Count > 0)
                     .ToList())
        {
            foreach (var group in item.Properties.Variants.Group)
            {
                if (!manager.TryGetValue<VariantGroup>(group, out var toApply))
                {
                    manager.FlagAsError(item, XmlError.ProcessingError, $"Variant group {group} does not exist");
                    ret.Errors[item.Guid] = $"Variant group {group} does not exist";
                    continue;
                }

                try
                {
                    var variants = toApply.ResolveVariants(item);
                    foreach (var variant in variants)
                    {
                        manager.Add(variant, variant.Name);
                        ret.AdditionalCount++;
                    }
                }
                catch (Exception ex)
                {
                    ret.Errors[item.Guid] = $"{item.Name}: failed to apply variant {toApply.Name}: {ex}";
                }
            }

            ret.TotalProcessed++;
        }

        manager.UpdateStatus<Item>(ret);
    }

    public static List<string> GenerateIds(string name) =>
        (from Gender gender in Enum.GetValues(typeof(Gender)) select GenerateId(name, gender)).ToList();

    public static string GenerateId(string name, Gender gender)
    {
        var rawhash = $"{name.Normalize().ToLower()}:{gender.ToString().Normalize()}";
        var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(rawhash));
        return string.Concat(hash.Select(selector: b => b.ToString("x2")))[..8];
    }

    public Item RandomVariant(string variant) =>
        Variants.TryGetValue(variant, out var value) ? value.PickRandom() : null;

    #region Accessors to provide defaults

    [XmlIgnore]
    public bool Stackable
    {
        get
        {
            if (Properties.Stackable != null) return Properties.Stackable.Max != 1;
            return false;
        }
    }

    [XmlIgnore] public int MaximumStack => Properties.Stackable?.Max ?? 0;

    [XmlIgnore] public byte MinLevel => Properties.Restrictions?.Level?.Min ?? 1;

    [XmlIgnore] public byte MinAbility => Properties.Restrictions?.Ab?.Min ?? 0;

    [XmlIgnore] public byte MaxLevel => Properties.Restrictions?.Level?.Max ?? 255;

    [XmlIgnore] public byte MaxAbility => Properties.Restrictions?.Level?.Max ?? 255;

    [XmlIgnore]
    public ElementType Element
    {
        get
        {
            var off = Properties.StatModifiers?.BaseOffensiveElement ?? ElementType.None;
            var def = Properties.StatModifiers?.BaseDefensiveElement ?? ElementType.None;
            return Properties.Equipment?.Slot == EquipmentSlot.Necklace ? off : def;
        }
    }

    [XmlIgnore] public bool Usable => Properties.Use != null;

    [XmlIgnore] public Use Use => Properties.Use;

    [XmlIgnore] public Class Class => Properties.Restrictions?.Class ?? Class.Peasant;

    [XmlIgnore] public Gender Gender => Properties.Restrictions?.Gender ?? Gender.Neutral;

    [XmlIgnore] public float MinLDamage => Properties.Damage?.LargeMin ?? 0;

    [XmlIgnore] public float MaxLDamage => Properties.Damage?.LargeMax ?? 0;

    [XmlIgnore] public float MinSDamage => Properties.Damage?.SmallMin ?? 0;

    [XmlIgnore] public float MaxSDamage => Properties.Damage?.SmallMax ?? 0;

    [XmlIgnore] public Variant CurrentVariant { get; set; }

    #endregion
}