using Hybrasyl.Xml.Manager;

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
