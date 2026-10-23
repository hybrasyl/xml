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
using System.Linq;

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

        variantItem.Properties.Flags |= Properties.Flags;

        var physical = variantItem.Properties.Physical;
        physical.Value = Math.Min(physical.Value * (Properties.Physical?.Value ?? 1), ushort.MaxValue);
        physical.Durability = Math.Min(physical.Durability * (Properties.Physical?.Durability ?? 1), ushort.MaxValue);
        physical.Weight = Math.Min(physical.Weight * (Properties.Physical?.Weight ?? 1), ushort.MaxValue);

        if (Properties.Restrictions?.Level is { } level)
        {
            variantItem.Properties.Restrictions ??= new ItemRestrictions();
            var baseLevel = variantItem.Properties.Restrictions.Level ??= new RestrictionsLevel();
            // The server treats an item with no minimum level as level 1.
            baseLevel.Min = (byte)Math.Min(99, Math.Max(1, (int)baseLevel.Min) + level.Min);
        }

        if (Properties.Appearance != null)
            variantItem.Properties.Appearance.Color = Properties.Appearance.Color;

        variantItem.Properties.StatModifiers ??= new StatModifiers();
        if (Properties.StatModifiers is { } modifiers)
        {
            variantItem.Properties.StatModifiers += modifiers;
            // The generated element properties have no Specified flag, so None is the only way to tell an
            // element the variant leaves unset; the clone already holds the base item's.
            if (modifiers.BaseDefensiveElement != ElementType.None)
                variantItem.Properties.StatModifiers.BaseDefensiveElement = modifiers.BaseDefensiveElement;
            if (modifiers.BaseOffensiveElement != ElementType.None)
                variantItem.Properties.StatModifiers.BaseOffensiveElement = modifiers.BaseOffensiveElement;
        }

        if (originalItem.Properties.Equipment?.Slot == EquipmentSlot.Weapon &&
            Properties.Damage is { } scale && variantItem.Properties.Damage is { } damage)
        {
            damage.LargeMin = ScaleDamage(damage.LargeMin, scale.LargeMin);
            damage.LargeMax = ScaleDamage(damage.LargeMax, scale.LargeMax);
            damage.SmallMin = ScaleDamage(damage.SmallMin, scale.SmallMin);
            damage.SmallMax = ScaleDamage(damage.SmallMax, scale.SmallMax);
        }

        variantItem.Properties.Tags.AddRange(Properties.Tags.Except(variantItem.Properties.Tags).ToList());
        return variantItem;
    }

    // Clamped before the cast, which would otherwise wrap an out-of-range value (80000 becomes 14464).
    private static ushort ScaleDamage(float value, float multiplier) =>
        (ushort)Math.Clamp(value * multiplier, 0, ushort.MaxValue);
}