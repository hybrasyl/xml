using System.Collections.Generic;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class VariantGroup : ILoadOnStart<VariantGroup>
{
    public Variant RandomVariant() => Variant.PickRandom();
    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<VariantGroup>.LoadAll(manager, path);
    public override string PrimaryKey => Name;

    public List<Item> ResolveVariants(Item originalItem)
    {
        var ret = new List<Item>();
        foreach (var variant in Variant)
        {
            var newItem = variant.ResolveVariant(originalItem);
            newItem.ParentGuid = originalItem.Guid;
            newItem.Variant = variant.Guid;
            ret.Add(newItem);
        }

        return ret;
    }

}