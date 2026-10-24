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
// (C) 2020-2026 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Hybrasyl.XmlTests;

/// <summary>Validates an XML string against every schema in <c>src/XSD</c>, which the test build copies to <c>XSD/</c>.</summary>
internal static class SchemaValidation
{
    public const string Ns = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02";

    private static readonly Lazy<XmlSchemaSet> Schemas = new(() =>
    {
        var set = new XmlSchemaSet();
        foreach (var file in Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "XSD"), "*.xsd"))
            set.Add(Ns, file);
        set.Compile();
        return set;
    });

    /// <returns>The schema errors. An empty list means the XML is valid.</returns>
    public static List<string> Validate(string xml)
    {
        var errors = new List<string>();
        var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = Schemas.Value };
        settings.ValidationEventHandler += (_, e) => errors.Add(e.Message);
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read()) { }
        return errors;
    }
}
