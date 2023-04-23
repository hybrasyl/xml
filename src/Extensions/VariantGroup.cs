using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class VariantGroup : ILoadOnStart<VariantGroup>
{
    public Variant RandomVariant() => Variant.PickRandom();
    public new static XmlLoadResult<VariantGroup> LoadAll(string path) => HybrasylEntity<VariantGroup>.LoadAll(path);

}