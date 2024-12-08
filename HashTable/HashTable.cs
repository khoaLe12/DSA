using System.Diagnostics;
using System.Collections;

namespace HashTable;

internal class HashTable : ICloneable
{
    // Hash function

    // Hash Table

    // Collision handler

    Hashtable newHashTable = new Hashtable();
    Dictionary<int, int> newDictionary = new Dictionary<int, int>();


    private const int InitialSize = 3;

    private struct bucket
    {
        public object? key;
        public object? value;
        public int hash_coll; // Store hash code; sign bit means there was a collision.
    }

    private bucket[] _buckets = null!;

    // The total number of entries in the hash table.
    private int _count;

    // The total number of collision bits set in the hashtable
    private int _occupancy;

    private int _loadsize;
    private float _loadFactor;

    private ICollection? _keys;
    private ICollection? _values;

    public HashTable() : this(InitialSize)
    {
    }

    public HashTable(int capacity) : this(capacity, 1.0f)
    {
    }

    public HashTable(int capacity, float loadFactor)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive number");

        if (loadFactor <= 0.0f || loadFactor > 1.0f)
            throw new ArgumentOutOfRangeException(nameof(loadFactor), "LoadFactor mus be in range of 0.1f to 1.0f");

        // Based on perf work, .72 is the optimal load factor for this table.
        _loadFactor = 0.72f * loadFactor;

        double rawsize = capacity / _loadFactor;
        if (rawsize > int.MaxValue)
            throw new ArgumentException("Capacity is overflow", nameof(capacity));

        // Avoid awfully small size
        int hashsize = (rawsize > InitialSize) ? (int)rawsize : InitialSize;
    }



    private int GetPrime(int number)
    {
        return number;
    }
}
