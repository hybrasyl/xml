# XML: Hybrasyl's Data Model / Entity Collection

This package provides a number of objects that can be used to work
with [Hybrasyl Server](https://github.com/hybrasyl/server)'s world
data (maps, items, NPCs, warps, etc). 

It can be found [on Nuget](https://www.nuget.org/packages/Hybrasyl.Xml)
XML schemas for Hybrasyl XML data can be found in
[XSD](https://github.com/hybrasyl/xml/tree/main/src/XSD). We use
[XmlSchemaClassGenerator](https://github.com/mganss/XmlSchemaClassGenerator)
(`dotnet-xscgen`) to generate C# classes from this collection of schema,
which are kept in the
[Objects](https://github.com/hybrasyl/xml/tree/main/src/Objects)
directory.  All of the generated classes are partial classes.

Extensions to these classes, which augment / add XML object functionality
used by [Hybrasyl Server](https://github.com/hybrasyl/server) can be
found in [Extensions](https://github.com/hybrasyl/xml/tree/main/src/Extensions).

## Regenerating

Generation runs headless on any platform:

```sh
dotnet tool install -g dotnet-xscgen
tools/regenerate.sh
```

`regenerate.sh` runs `xscgen` over every schema and then
`tools/patch-flags-enums.py`, which applies the wire-format fixups
XmlSerializer needs but xscgen cannot express from the schema alone
(space-separated `xs:list` members, and the child objects whose XSD
defaults carry game semantics). The patch script is strict: if the
generator's output shape changes, it fails loudly and writes nothing
rather than emitting a subtly wrong model.

Some types are maintained by hand and are not generated: the flags enums
in [Enums](https://github.com/hybrasyl/xml/tree/main/src/Enums),
`HybrasylEntity` and its per-type bases file. Each says so in its header.

## Contributing

Open a PR! If it involves XSD changes, regenerate with the steps above
and include the regenerated `Objects` in your PR.
