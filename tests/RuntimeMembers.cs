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

using System;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

/// <summary>The runtime helpers of the partial classes, which Serialize does not write.</summary>
public class RuntimeMemberTests
{
    [Fact]
    public void CastableEffectListsAreEmptyWithoutEffects()
    {
        var castable = new Castable { Effects = null };

        Assert.Empty(castable.AddStatuses);
        Assert.Empty(castable.RemoveStatuses);
        Assert.Empty(castable.Reactors);
    }

    [Fact]
    public void CastableEffectListsAreEmptyWithoutStatuses()
    {
        var castable = new Castable { Effects = new CastableEffects { Statuses = null, Reactors = null } };

        Assert.Empty(castable.AddStatuses);
        Assert.Empty(castable.RemoveStatuses);
        Assert.Empty(castable.Reactors);
    }

    [Fact]
    public void CastableEffectListsReadTheEffects()
    {
        var add = new AddStatus { Value = "Poison" };
        var remove = new RemoveStatus { Value = "Sleep" };
        var reactor = new CastableReactor();
        var castable = new Castable
        {
            Effects = new CastableEffects
            {
                Statuses = new Statuses { Add = [add], Remove = [remove] },
                Reactors = [reactor]
            }
        };

        Assert.Same(add, Assert.Single(castable.AddStatuses));
        Assert.Same(remove, Assert.Single(castable.RemoveStatuses));
        Assert.Same(reactor, Assert.Single(castable.Reactors));
    }

    [Fact]
    public void CastableKeepsItsLevelAndLastCast()
    {
        var castable = new Castable { Cooldown = 60, CastableLevel = 4, LastCast = DateTime.Now };

        Assert.Equal(4, castable.CastableLevel);
        Assert.True(castable.OnCooldown);
    }

    [Fact]
    public void AccessWithAWildcardIsAllPrivileged()
    {
        var access = new Access { Privileged = [" * "] };

        Assert.False(access.AllPrivileged);
        Assert.Contains("*", access.PrivilegedUsers);
        Assert.True(access.AllPrivileged);
    }

    [Fact]
    public void TraitSecondaryKeyIsTheLowercaseName()
    {
        Assert.Equal(["dockworker"], new Trait { Name = "Dockworker" }.SecondaryKeys);
        Assert.Empty(new Trait { Name = null }.SecondaryKeys);
    }
}
