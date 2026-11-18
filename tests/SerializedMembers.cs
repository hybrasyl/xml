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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class SerializedMemberTests
{
    // An attribute that maps a member to the schema. The generator puts one on each schema member.
    private static readonly Type[] MappingAttributes =
    [
        typeof(XmlElementAttribute), typeof(XmlAttributeAttribute), typeof(XmlArrayAttribute),
        typeof(XmlArrayItemAttribute), typeof(XmlTextAttribute), typeof(XmlAnyElementAttribute),
        typeof(XmlAnyAttributeAttribute), typeof(XmlChoiceIdentifierAttribute)
    ];

    private static bool IsCollection(Type type) =>
        type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);

    // The members that XmlSerializer writes: public read-write properties and fields, and
    // public get-only properties of a collection type.
    private static IEnumerable<MemberInfo> WrittenMembers(Type type)
    {
        foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (p.GetIndexParameters().Length > 0 || p.GetMethod is not { IsPublic: true }) continue;
            if (p.SetMethod is { IsPublic: true } || IsCollection(p.PropertyType)) yield return p;
        }

        foreach (var f in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            if (!f.IsInitOnly) yield return f;
    }

    // XmlSerializer reads the attributes of the declaration it finds, not of an overridden base
    // member, so an override of an ignored member must repeat [XmlIgnore].
    private static bool IsIgnored(MemberInfo m) => Attribute.IsDefined(m, typeof(XmlIgnoreAttribute), false);

    private static Type MemberType(MemberInfo m) => m is PropertyInfo p ? p.PropertyType : ((FieldInfo) m).FieldType;

    private static IEnumerable<Type> ReachableTypes()
    {
        var assembly = typeof(Castable).Assembly;
        var queue = new Queue<Type>(assembly.GetTypes().Where(t => t.GetCustomAttribute<XmlRootAttribute>() != null));
        var seen = new HashSet<Type>();
        while (queue.Count > 0)
        {
            var type = queue.Dequeue();
            if (!seen.Add(type)) continue;
            yield return type;
            foreach (var member in WrittenMembers(type))
            {
                if (IsIgnored(member)) continue;
                var memberType = MemberType(member);
                var element = memberType.IsGenericType ? memberType.GetGenericArguments()[0] : memberType;
                if (element.Assembly == assembly && element.IsClass) queue.Enqueue(element);
            }
        }
    }

    [Fact]
    public void SerializeWritesOnlySchemaMembers()
    {
        var unmapped = (from type in ReachableTypes()
            from member in WrittenMembers(type)
            where !IsIgnored(member)
            where !MappingAttributes.Any(a => Attribute.IsDefined(member, a, false))
            select $"{type.Name}.{member.Name}").Distinct().OrderBy(n => n).ToList();

        Assert.True(unmapped.Count == 0, "Add [XmlIgnore] to: " + string.Join(", ", unmapped));
    }

    public static IEnumerable<object[]> RootTypes() =>
        typeof(Castable).Assembly.GetTypes().Where(t => t.GetCustomAttribute<XmlRootAttribute>() != null)
            .Select(t => new object[] { t.Name });

    [Theory]
    [MemberData(nameof(RootTypes))]
    public void SerializeWritesNoBaseMember(string typeName)
    {
        // An override of SecondaryKeys must not lose the [XmlIgnore] of the base member.
        var type = typeof(Castable).Assembly.GetTypes().Single(t => t.Name == typeName && t.IsDefined(typeof(XmlRootAttribute)));
        var entity = Activator.CreateInstance(type)!;
        var xml = (string) type.GetMethod("Serialize", Type.EmptyTypes)!.Invoke(entity, null)!;

        var names = XDocument.Parse(xml).Descendants().Select(e => e.Name.LocalName).ToHashSet();
        Assert.DoesNotContain("Guid", names);
        Assert.DoesNotContain("LoadPath", names);
        Assert.DoesNotContain("Error", names);
        Assert.DoesNotContain("LoadErrorMessage", names);
        Assert.DoesNotContain("SecondaryKeys", names);
    }

    [Fact]
    public void SerializedSpawnGroupValidates()
    {
        var group = new SpawnGroup { Name = "Crypt", BaseLevel = "10", LoadPath = "spawngroups/crypt.xml" };
        group.LoadErrorMessage = "an error";

        var xml = group.Serialize();

        Assert.Empty(SchemaValidation.Validate(xml));
        Assert.Empty(XDocument.Parse(xml).Root!.Elements());
    }
}
