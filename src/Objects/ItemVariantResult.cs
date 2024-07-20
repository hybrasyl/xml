namespace Hybrasyl.Xml.Objects
{
    public record ItemVariantResult(Item Variant = null, string Error = null)
    {
        public Item Variant { get; set; } = Variant;
        public string Error { get; set; } = Error ?? string.Empty;
    }
}
