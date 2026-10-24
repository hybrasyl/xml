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
// (C) 2020-2026 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System.Collections.Generic;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Trait : ILoadOnStart<Trait>, IAdditionalValidation<Trait>
{
    public override string PrimaryKey => Name;

    public override List<string> SecondaryKeys => Name == null ? [] : [Name.ToLower()];

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Trait>.LoadAll(manager, path);

    /// <summary>Run <see cref="Validate" /> on each trait. The manager runs it at load.</summary>
    public static void ValidateAll(IWorldDataManager manager)
    {
        var result = new XmlValidationResult();
        foreach (var trait in manager.Values<Trait>())
        {
            result.TotalProcessed++;
            var errors = trait.Validate(manager);
            if (errors.Count > 0)
                result.Errors.Add(trait.Guid, $"Trait {trait.Name}: {string.Join(" ", errors)}");
        }

        manager.UpdateResult<Trait>(result);
    }

    /// <summary>
    ///     Check the rules that need the other traits, and the rules that the schema has but the loader does not
    ///     check.
    /// </summary>
    /// <returns>One message for each problem. An empty list means the trait is correct.</returns>
    public List<string> Validate(IWorldDataManager manager)
    {
        var errors = new List<string>();
        if (ExclusiveGroup != null && string.IsNullOrEmpty(ExclusiveGroup.Value))
            errors.Add("ExclusiveGroup has no name.");
        foreach (var name in ConflictsWith ?? [])
        {
            if (name == Name)
                errors.Add("ConflictsWith names the trait itself.");
            else if (!manager.ContainsKey<Trait>(name))
                errors.Add($"ConflictsWith names {name}, which is not a trait.");
        }

        return errors;
    }
}
