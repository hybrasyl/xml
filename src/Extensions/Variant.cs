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

namespace Hybrasyl.Xml.Objects;

public partial class Variant
{
    public override string PrimaryKey => Name;

    public Item ResolveVariant(Item originalItem)
    {
        var variantItem = originalItem.Clone<Item>(true);

        variantItem.Name = $"{Modifier} {originalItem.Name}";
        variantItem.ParentGuid = originalItem.Guid;
        variantItem.IsVariant = true;

        if (variantItem.Properties.Flags != 0)
            variantItem.Properties.Flags = Properties.Flags;

        var newValue = originalItem.Properties.Physical.Value * Properties.Physical?.Value ?? 1;
        var newDura = originalItem.Properties.Physical.Durability * Properties.Physical?.Durability ?? 1;
        var newWeight = originalItem.Properties.Physical.Weight * Properties.Physical?.Weight ?? 1;

        variantItem.Properties.StatModifiers ??= new StatModifiers();

        variantItem.Properties.Physical.Value = newValue > ushort.MaxValue ? ushort.MaxValue : newValue;
        variantItem.Properties.Physical.Durability = newDura > ushort.MaxValue ? ushort.MaxValue : newDura;
        variantItem.Properties.Physical.Weight = newWeight > ushort.MaxValue ? ushort.MaxValue : newWeight;

        // ensure boot hiding is carried to variants
        variantItem.Properties.Appearance.HideBoots = originalItem.Properties.Appearance.HideBoots;
        if (Properties.Restrictions?.Level != null)
            variantItem.Properties.Restrictions.Level.Min = (byte)Math.Min(99,
                variantItem.Properties.Restrictions.Level.Min + Properties.Restrictions.Level.Min);

        if (Properties.Appearance != null)
            variantItem.Properties.Appearance.Color = Properties.Appearance.Color;

        if (Properties.StatModifiers != null)
            variantItem.Properties.StatModifiers += Properties.StatModifiers;

        if (originalItem.Properties.Equipment.Slot == EquipmentSlot.Weapon && Properties.Damage != null)
        {
            variantItem.Properties.Damage.LargeMin =
                (ushort)(originalItem.Properties.Damage.LargeMin * Properties.Damage.LargeMin);
            variantItem.Properties.Damage.LargeMax =
                (ushort)(originalItem.Properties.Damage.LargeMax * Properties.Damage.LargeMax);
            variantItem.Properties.Damage.SmallMin =
                (ushort)(originalItem.Properties.Damage.SmallMin * Properties.Damage.SmallMin);
            variantItem.Properties.Damage.SmallMin =
                (ushort)(originalItem.Properties.Damage.SmallMin * Properties.Damage.SmallMin);
        }

        if (Properties.StatModifiers?.BaseDefensiveElement != null)
            variantItem.Properties.StatModifiers.BaseDefensiveElement =
                Properties.StatModifiers.BaseDefensiveElement;
        else
            variantItem.Properties.StatModifiers.BaseDefensiveElement =
                originalItem.Properties.StatModifiers?.BaseDefensiveElement ?? ElementType.None;

        if (Properties.StatModifiers?.BaseOffensiveElement != null)
            variantItem.Properties.StatModifiers.BaseOffensiveElement =
                Properties.StatModifiers.BaseOffensiveElement;
        else
            variantItem.Properties.StatModifiers.BaseDefensiveElement =
                Properties.StatModifiers?.BaseOffensiveElement ?? ElementType.None;

        variantItem.Properties.Tags.AddRange(Properties.Tags);
        return variantItem;
    }
}