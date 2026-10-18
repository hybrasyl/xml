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

using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class ExperienceScaling
{
    [Theory]
    [InlineData(20, 1)]
    [InlineData(6, 1)]
    [InlineData(5, 40)]
    [InlineData(4, 80)]
    [InlineData(3, 100)]
    [InlineData(0, 100)]
    [InlineData(-3, 100)]
    [InlineData(-4, 105)]
    [InlineData(-5, 110)]
    [InlineData(-6, 115)]
    [InlineData(-7, 120)]
    [InlineData(-20, 120)]
    public void ScaleExperience_UsesHybrasylScale_WhenConfigHasNoSteps(int levelDifference, uint expected)
    {
        var config = new ServerConfig();

        Assert.Equal(expected, config.ScaleExperience(100, levelDifference));
    }

    [Theory]
    [InlineData(5, 80)]
    [InlineData(7, 60)]
    [InlineData(13, 20)]
    [InlineData(15, 1)]
    [InlineData(3, 100)]
    [InlineData(-10, 100)]
    public void ScaleExperience_UsesConfiguredSteps_InPlaceOfHybrasylScale(int levelDifference, uint expected)
    {
        var config = new ServerConfig
        {
            ExperienceScaling =
            {
                ExperienceScalingStep.Create(4, 5, 80),
                ExperienceScalingStep.Create(6, 8, 60),
                ExperienceScalingStep.Create(9, 11, 40),
                ExperienceScalingStep.Create(12, 14, 20),
                ExperienceScalingStep.Create(15, null, fixedAmount: 1)
            }
        };

        Assert.Equal(expected, config.ScaleExperience(100, levelDifference));
    }

    [Fact]
    public void ScaleExperience_UsesFirstCoveringStep_WhenStepsOverlap()
    {
        var config = new ServerConfig
        {
            ExperienceScaling =
            {
                ExperienceScalingStep.Create(0, 10, 50),
                ExperienceScalingStep.Create(5, 5, 10)
            }
        };

        Assert.Equal(50u, config.ScaleExperience(100, 5));
    }

    [Fact]
    public void ScaleExperience_ReadsStepsFromXml()
    {
        var config = ServerConfig.Deserialize("""
            <ServerConfig xmlns="http://www.hybrasyl.com/XML/Hybrasyl/2020-02">
              <ExperienceScaling>
                <Step Min="6" Max="8" Percent="60" />
                <Step Min="15" Fixed="1" />
              </ExperienceScaling>
            </ServerConfig>
            """);

        Assert.Equal(60u, config.ScaleExperience(100, 7));
        Assert.Equal(1u, config.ScaleExperience(100, 30));
        Assert.Equal(100u, config.ScaleExperience(100, 10));
    }

    [Fact]
    public void Apply_RoundsUpFromTheExactPercentage()
    {
        // 100 * 1.10 in double is 110.00000000000001, which a ceiling makes 111.
        Assert.Equal(110u, ExperienceScalingStep.Create(null, null, 110).Apply(100));
        Assert.Equal(3u, ExperienceScalingStep.Create(null, null, 40).Apply(7));
    }

    [Fact]
    public void Apply_ClampsToTheLargestExperience()
    {
        Assert.Equal(uint.MaxValue, ExperienceScalingStep.Create(null, null, 200).Apply(uint.MaxValue));
    }
}
