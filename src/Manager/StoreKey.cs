using System;

namespace Hybrasyl.Xml.Manager;

public record StoreKey
{
    public string Key { get; init; }
    public bool IsPrimaryKey { get; init; }

    public StoreKey(dynamic key, bool isPrimaryKey)
    {
        Key = key.ToString();
        IsPrimaryKey = isPrimaryKey;
    }

    public virtual bool Equals(StoreKey other) =>
        other != null && Key == other.Key && IsPrimaryKey == other.IsPrimaryKey && GetHashCode() == other.GetHashCode();
    public override int GetHashCode() => HashCode.Combine(Key, IsPrimaryKey);
}