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

using Hybrasyl.Xml.Manager;
using Xunit;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class StoreKeyTests
{
    [Fact]
    public void StoreKeyEquality()
    {
        var s1 = new StoreKey(1234, true);
        var s2 = new StoreKey(1234, true);
        Assert.Equal(s1, s2);
        var s3 = new StoreKey(1234, false);
        Assert.NotEqual(s1, s3);
        var s4 = new StoreKey(1234, false);
        Assert.Equal(s3, s4);
        var s5 = new StoreKey("1234", true);
        Assert.Equal(s1, s5);
    }
}