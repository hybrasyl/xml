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
using Xunit;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class LocalizationTests
{
    public LocalizationTests()
    {
        // Ceridwen ships a "default" localization file that we use for testing here
        Locale = Localization.LoadFromFile(Path.Join(Settings.XmlTests.JsonSettings.WorldDataDirectory, "localizations",
            "en_us.xml"));
        Assert.NotNull(Locale);
    }

    public Localization Locale { get; set; }

    [Fact]
    public void GetString()
    {
        var greeting = Locale.Merchant.FirstOrDefault(predicate: x => x.Key == "greeting");
        Assert.NotNull(greeting);
        Assert.Equal(greeting.Value, Locale.GetString("greeting"));
    }

    [Fact]
    public void GetResponse()
    {
        var response = Locale.NpcResponses.FirstOrDefault(predicate: x => x.Call == "What is your name?");
        Assert.NotNull(response);
        Assert.Equal(response.Value, Locale.GetResponse("What is your name?"));
        Assert.Equal(response.Value, Locale.GetResponse("WhAt iS yOuR NaMe?"));
        Assert.Equal(response.Value, Locale.GetResponse("WhAt iS yOuR NaMe?               "));
    }
}