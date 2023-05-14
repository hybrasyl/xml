using System.Reflection.Metadata.Ecma335;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class Localization : ILoadOnStart<Localization>
{
    public string PrimaryKey => Locale;

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Localization>.LoadAll(manager, path);
}

