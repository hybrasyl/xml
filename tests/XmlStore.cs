using System.Security.Cryptography;
using Hybrasyl.Xml;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Assert = Xunit.Assert;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlStoreTests
{
    private Castable Castable { get; } = new()  { Book = Book.PrimarySkill, Name = "Test Skill" };

    private Castable RandomCastable
    {
        get
        {
            var values = Enum.GetValues(typeof(Book));
            var booktype = (Book)(values.GetValue(Random.Shared.Next(values.Length)) ?? Book.PrimarySkill);
            return new Castable { Book = booktype, Name = $"Test Castable {Random.Shared.Next(0, 1000)}" };
        }
    }

    private XmlDataStore<Castable> GetCastableStore()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        return store;
    }

    [Fact]
    public void GetSet()
    {
        // Fetch store
        var store = GetCastableStore();
        // Set by name
        store.Add(Castable, Castable.Name);
        // Retrieve by name, ensure object is the same
        var retrieve = store.Get(Castable.Name);
        Assert.NotNull(retrieve);
        Assert.Equal(retrieve.Guid, Castable.Guid);
        // Ensure count / values are correct
        Assert.Equal(1, store.Count);
        Assert.Single(store.Items);
        Assert.Equal(1, store.Keys.Count);
        Assert.NotNull(store[Castable.Name]);
    }

    [Fact]
    public void GetSetIndex()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        store.AddWithIndex(Castable, Castable.Name, "1234", 1234, 1234.0);
        var retrieve = store.Get(Castable.Name);
        Assert.NotNull(retrieve);
        Assert.Equal(store.Get(Castable.Name).Guid, Castable.Guid);
        retrieve = store.GetByIndex(Castable.Name);
        Assert.Null(retrieve);
        retrieve = store.GetByIndex("1234");
        Assert.NotNull(retrieve);
        Assert.Equal(store.Get(Castable.Name).Guid, Castable.Guid);
        retrieve = store.GetByIndex(1234);
        Assert.NotNull(retrieve);
        Assert.Equal(store.Get(Castable.Name).Guid, Castable.Guid);
        retrieve = store.GetByIndex(1234.0);
        Assert.NotNull(retrieve);
        Assert.Equal(store.Get(Castable.Name).Guid, Castable.Guid);
    }

    [Fact]
    public void SetMultiple()
    {
        var store = new XmlDataStore<Castable>();
        var c1 = RandomCastable;
        store.Add(Castable, Castable.Name);
        Assert.NotNull(store);
        var retrieve = store.Get(Castable.Name);
        Assert.NotNull(retrieve);
        Assert.Equal(store.Get(Castable.Name).Guid, Castable.Guid);
        store.Add(RandomCastable, Castable.Name);
        var retrieve2 = store.Get(Castable.Name);
        Assert.NotNull(retrieve2);
        Assert.NotEqual(retrieve2.Guid, Castable.Guid);
    }

    [Fact]
    public void SetMultipleWithIndex()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        var c1 = RandomCastable;
        // Store with index
        store.AddWithIndex(Castable, Castable.Name, "1234", 12345, 12346.0);
        // Ensure each index works for retrieval
        var r1 = store.GetByIndex("1234");
        var r2 = store.GetByIndex(12345);
        var r3 = store.GetByIndex(12346.0);
        // Assert all of the index retrievals were successful
        Assert.NotNull(r1);
        Assert.NotNull(r2);
        Assert.NotNull(r3);
        // Assert the guids are equal for all the retrievals
        Assert.Equal(r1.Guid, Castable.Guid);
        Assert.Equal(r2.Guid, Castable.Guid);
        Assert.Equal(r3.Guid, Castable.Guid);
        // Update one of the indexes with a new item
        store.AddWithIndex(RandomCastable, Castable.Name, "1234");
        var retrieve2 = store.Get(Castable.Name);
        // Should be able to retrieve new item by name and index, and should not be same guid as old item
        Assert.NotNull(retrieve2);
        Assert.NotEqual(retrieve2.Guid, Castable.Guid);
        Assert.NotNull(store.GetByIndex(1234));
        Assert.NotEqual(Castable.Guid, store.GetByIndex(1234).Guid);
        // Old indexes should have been cleaned up
        Assert.Null(store.GetByIndex(12345.0));
        Assert.Null(store.GetByIndex(12346.0));
    }

    [Fact]
    public void SetMultipleAndRemove()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        var c1 = RandomCastable;
        var c2 = RandomCastable;
        store.AddWithIndex(c1, c1.Name, "1234", 12345, 12345.6);
        store.AddWithIndex(c2, c2.Name, "123456", 1234567, 1234567.8);
        Assert.True(store.Remove(c1.Name));
        Assert.True(store.Remove(c2.Name));
        Assert.Equal(0, store.Count);
        Assert.Null(store.Get(c1.Name));
        Assert.Null(store.Get(c2.Name));
        Assert.Null(store[c1.Name]);
        Assert.Null(store[c2.Name]);
        Assert.Null(store.GetByIndex("1234"));
        Assert.Null(store.GetByIndex("12345"));
        Assert.Null(store.GetByIndex("12345.6"));
        Assert.Null(store.GetByIndex("123456"));
        Assert.Null(store.GetByIndex("123457"));
        Assert.Null(store.GetByIndex("1234567.8"));
    }

    [Fact]
    public void SetRemove()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        var c1 = RandomCastable;
        store.Add(Castable, Castable.Name);
        store.Add(c1, c1.Name);
        Assert.Equal(2, store.Count);
        store.Remove(Castable.Name);
        Assert.Equal(1, store.Count);
        Assert.Null(store.Get(Castable.Name));
        store.Remove(c1.Name);
        Assert.Empty(store.Items);
        Assert.Equal(0, store.Count);
    }

    [Fact]
    public void SetRemoveIndex()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        var c1 = RandomCastable;
        store.AddWithIndex(Castable, Castable.Name, "1234", 1234, 1234.0);
        store.AddWithIndex(c1, c1.Name, "12345", 12345, 12345.0);
        store.RemoveByIndex(1234.0);
        Assert.Equal(1, store.Count);
        Assert.Throws<KeyNotFoundException>(() => store.Get(Castable.Name));
        Assert.Throws<KeyNotFoundException>(() => store.GetByIndex("1234"));
        Assert.Throws<KeyNotFoundException>(() => store.GetByIndex(1234));
        Assert.Throws<KeyNotFoundException>(() => store.GetByIndex(1234.0));
    }

    [Fact]
    public void SetTryGetValue()
    {
        var store = new XmlDataStore<Castable>();
        Assert.NotNull(store);
        var c1 = RandomCastable;
        store.Add(Castable, Castable.Name);
        Assert.True(store.TryGetValue(Castable.Name, out var c2));
        Assert.NotNull(c2);
    }

    [Fact]
    public void SetTryGetValueByIndex()
    {
        var store = new XmlDataStore<Castable>();
        store.Add(Castable, Castable.Name);
        Assert.False(store.TryGetValueByIndex("1234", out var c2));
        Assert.Null(c2);
    }

    [Fact]
    public void SetIndexTryGetValueByIndex()
    {
        var store = GetCastableStore();
        store.AddWithIndex(Castable, Castable.Name, "1234", 12345, 12345.6);
        Assert.True(store.TryGetValueByIndex("1234", out var c2));
        Assert.True(store.TryGetValueByIndex(12345, out var c3));
        Assert.True(store.TryGetValueByIndex(12345.6, out var c4));
        Assert.False(store.TryGetValueByIndex(12345.67, out var c5));
        Assert.NotNull(c2);
        Assert.NotNull(c3);
        Assert.NotNull(c4);
    }

    [Fact]
    public void SetContainsKey()
    {
        var store = GetCastableStore();
        store.Add(Castable, Castable.Name);
        Assert.True(store.ContainsKey(Castable.Name));
    }

    [Fact]
    public void SetContainsIndex()
    {
        var store = GetCastableStore();

        store.AddWithIndex(Castable, Castable.Name, "1234", 12345, 12345.6);
        Assert.True(store.ContainsIndex("1234"));
        Assert.True(store.ContainsIndex(12345));
        Assert.True(store.ContainsIndex(12345.6));
        Assert.False(store.ContainsIndex(12345.67));
    }

    [Fact]
    public void SetCount()
    {
        var store = GetCastableStore();

        store.Add(Castable, Castable.Name);
        Assert.Equal(1, store.Count);
    }

    [Fact]
    public void SetRemoveCount()
    {
        var store = GetCastableStore();

        store.Add(Castable, Castable.Name);
        Assert.Equal(1, store.Count);
        store.Remove(Castable.Name);
        Assert.Equal(0, store.Count);

    }

    [Fact]
    public void SetRemoveByIndexCount()
    {
        var store = GetCastableStore();

        store.AddWithIndex(Castable, Castable.Name, "1234", 12345, 12345.6);
        Assert.Equal(1, store.Count);
        store.RemoveByIndex(12345);
        Assert.Equal(0, store.Count);
    }

    [Fact]
    public void SetAndRemoveItems()
    {
        var store = GetCastableStore();

        var c1 = RandomCastable;
        var c2 = RandomCastable;
        store.AddWithIndex(Castable, Castable.Name, "1234", 12345, 12345.6);
        store.AddWithIndex(c1, c1.Name, "12345678");
        store.AddWithIndex(c2, c2.Name, "123456789");
        Assert.Equal(3, store.Count);
        Assert.Contains(Castable, store.Items);
        Assert.Contains(c1, store.Items);
        Assert.Contains(c2, store.Items);
        Assert.Equal(3, store.Items.Count());
        Assert.True(store.Remove(Castable.Name));
        Assert.DoesNotContain(Castable, store.Items);
        Assert.True(store.Remove(c1.Name));
        Assert.DoesNotContain(c1, store.Items);
        Assert.True(store.Remove(c2.Name));
        Assert.DoesNotContain(c2, store.Items);
        Assert.Empty(store.Items);
    }

    [Fact]
    public void FlagAsError()
    {
        var store = GetCastableStore();
        var c1 = RandomCastable;
        var c2 = RandomCastable;
        store.Add(c1);
        store.Add(c2);
        store.FlagAsError(c1.Guid,XmlError.SyntaxError,"idk man");
        Assert.Equal("idk man",c1.LoadErrorMessage);
        Assert.Single(store.Errors);
        Assert.Equal(c1.Guid, store.Errors.First().Guid);
        Assert.Equal(XmlError.SyntaxError,c1.Error);
        Assert.Equal(XmlError.None,c2.Error);
    }

    [Fact]
    public void AddEntity()
    {
        var store = new XmlDataStore<Castable>();
        var c1 = RandomCastable;
        store.Add(c1);
        Assert.Contains(c1, store.Items);
        Assert.NotNull(store.Get(c1.Id));
    }

    [Fact]
    public void GetByFilename()
    {
        var store = GetCastableStore();

        var c1 = RandomCastable;
        c1.LoadPath = "/hurr/durr/get-by-filename.xml";
        Assert.Equal("get-by-filename.xml", c1.Filename);
        store.Add(c1);
        Assert.Contains(c1, store.Items);
        var ret = store.GetByFilename(c1.Filename);
        Assert.NotNull(ret);
        Assert.Equal(c1.Guid, ret.Guid);
    }

    [Fact]
    public void GetByGuid()
    {
        var store = GetCastableStore();

        var c1 = RandomCastable;
        store.Add(c1);
        Assert.Contains(c1, store.Items);
        var ret = store.GetByGuid(c1.Guid);
        Assert.NotNull(ret);
        Assert.Equal(c1.Guid, ret.Guid);
        Assert.Null(store.GetByGuid(Guid.NewGuid()));
    }

    [Fact]
    public void TryGetValueByFilename()
    {
        var store = GetCastableStore();

        var c1 = RandomCastable;
        c1.LoadPath = "/hurr/durr/get-by-filename.xml";
        Assert.Equal("get-by-filename.xml", c1.Filename);
        store.Add(c1);
        Assert.Contains(c1, store.Items);
        Assert.True(store.TryGetValueByFilename(c1.Filename, out var ret));
        Assert.NotNull(ret);
        Assert.Equal(c1.Guid, ret.Guid);    }

    [Fact]
    public void Clear()
    {
        var store = GetCastableStore();
        var c1 = RandomCastable;
        var c2 = RandomCastable;
        store.Add(c1);
        store.Add(c2);
        Assert.Contains(c1, store.Items);
        Assert.Contains(c2, store.Items);
        store.Clear();
        Assert.DoesNotContain(c1, store.Items);
        Assert.DoesNotContain(c2, store.Items);
        Assert.Empty(store.Items);
    }

    [Fact]
    public void Contains()
    {
        var store = GetCastableStore();
        var c1 = RandomCastable;
        var c2 = RandomCastable;
        store.Add(c1);
        store.Add(c2);
        Assert.Contains(c1, store.Items);
        Assert.Contains(c2, store.Items);
        Assert.True(store.Contains(c1));
        Assert.True(store.Contains(c2));
        var c3 = RandomCastable;
        Assert.DoesNotContain(c3, store.Items);
        Assert.False(store.Contains(c3));
    }

    [Fact]
    public void GetByCategory()
    {
        var store = GetCastableStore();
        var c1 = RandomCastable;
        var c2 = RandomCastable;
        c1.Categories.Add(new Category { Value="Category 1"});
        c1.Categories.Add(new Category { Value="Category 2"});
        c2.Categories.Add(new Category { Value="Category 1"});
        Assert.Contains("category 1", c1.CategoryList);
        Assert.Contains("category 2", c1.CategoryList);
        store.Add(c1,c1.Name);
        store.Add(c2,c2.Name);
        var c1e = store.GetByCategory("category 1").ToList();
        Assert.NotEmpty(c1e);
        Assert.Equal(c1e.First().Guid, c1.Guid);
        Assert.Equal(2, c1e.Count());
        var c2e = store.GetByCategory("category 2").ToList();
        Assert.NotEmpty(c2e);
        Assert.Equal(c2e.First().Guid, c1.Guid);
        Assert.Single(c2e);
        var c3e = store.GetByCategory("category 3").ToList();
        Assert.Empty(c3e);
    }
}
