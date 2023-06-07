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

using Hybrasyl.Xml.Objects;

namespace Hybrasyl.XmlTests;

public class Entities
{
    [Fact]
    public void GuidNonEmpty()
    {
        var castable = new Castable();
        Assert.NotNull(castable);
        Assert.NotEqual(castable.Guid, Guid.Empty);
    }

    [Fact]
    public void Resistances()
    {
        var modifier = new StatModifiers();
        Assert.Empty(modifier.Augments);
        Assert.Empty(modifier.Resistances);
    }
}