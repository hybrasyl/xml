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
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class VariantGroup : ILoadOnStart<VariantGroup>
{
    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<VariantGroup>.LoadAll(manager, path);

    public Variant RandomVariant() => Variant.PickRandom();

    public List<Item> ResolveVariants(Item originalItem)
    {
        var ret = new List<Item>();
        foreach (var variant in Variant)
            try
            {
                var newItem = variant.ResolveVariant(originalItem);
                newItem.ParentGuid = originalItem.Guid;
                newItem.Variant = variant.Guid;
                ret.Add(newItem);
            }
            catch (Exception ex) { }

        return ret;
    }
}