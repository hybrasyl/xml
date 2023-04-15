# XML: Hybrasyl's Data Model / Entity Collection

This package provides a number of objects that can be used to work
with [Hybrasyl Server](https://github.com/hybrasyl/server)'s world
data (maps, items, NPCs, warps, etc). 

XML schemas for Hybrasyl XML data can be found in
[XSD](https://github.com/hybrasyl/xml/tree/main/XSD). We use
[xsd2code++](https://www.xsd2code.com/) to generate C# classes from
this collection of schema, which are kept in the
[Objects](https://github.com/hybrasyl/xml/tree/main/Objects)
directory.  All of the generated classes are partial classes.

Extensions to these classes, which augment XML object functionality
used by [Hybrasyl Server](https://github.com/hybrasyl/server) can be
found in [Extensions](https://github.com/hybrasyl/xml/tree/main/Extensions).

## Contributing

Open a PR! If it involves XSD changes, a project maintainer will run
xsd2code for you. 
