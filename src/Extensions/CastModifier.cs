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
// (C) 2020-2023 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class CastModifier
{
    // The XSD models Add/Subtract/Replace as an xs:choice; the old generator
    // exposed whichever element was present as a single object-typed Item,
    // and the server pattern-matches on it. The typed trio carries the wire.
    [XmlIgnore]
    [JsonIgnore]
    public object Item => (object)Add ?? (object)Subtract ?? Replace;
}
