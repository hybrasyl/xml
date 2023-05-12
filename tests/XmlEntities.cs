using Hybrasyl.Xml.Objects;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlEntityTests
{
    // xsd2code has a few oddities / bugs that result in the wrong type being used or 
    // a type being a list when it is a singular object. These tests ensure nothing gets
    // committed and pushed out to Nuget with those errors.
    [Fact]
    public void CreatureAssailSoundIsByte()
    {
        // Occasionally xsd2code will make this an sbyte, for unknown reasons,
        // so we test for that hre
        var f = new Creature();
        Assert.IsType<byte>(f.AssailSound);
    }

    [Fact]
    public void ElementSourceTableIsNotList()
    {
        var check = new ElementTableSourceElement();
        Assert.IsType<ElementType>(check.Element);
    }

    [Fact]
    public void ElementTargetTableIsNotList()
    {
        var check = new ElementTableTargetElement();
        Assert.IsType<ElementType>(check.Element);
    }

}
