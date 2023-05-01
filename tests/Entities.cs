using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}

