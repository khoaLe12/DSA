using System.Diagnostics;
using System.Collections;

namespace HashTable;

internal class HashTable
{
    Hashtable newHashTable = new Hashtable();
    Dictionary<int, int> newDictionary = new Dictionary<int, int>();


    private const int InitialSize = 3;

    private struct bucket
    {
        public object? key;
        public object? value;
    }

    private bucket[] _buckets = null!;

    // The total number of entries in the hash table.
    private int _count;

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

    // Initialize hash table with the size of prime number of capacity
    // The load factor is used to limit the load size, helps to prevent double hashing to become linear probing
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
        int hashsize = (rawsize > InitialSize) ? GetPrime((int)rawsize) : InitialSize;
        _buckets = new bucket[hashsize];

        _loadsize = (int)(hashsize * _loadFactor);
    }

    public virtual object? this[object key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key must not be null");

            uint hashCode = InitHash(key, _buckets.Length, out uint seed, out uint incr);
            int ntry = 0;
            int collisionCount = 0;
            bucket b;
            int bucketNumber = (int)seed;

            do
            {
                b = _buckets[bucketNumber];

                if (b.key == null)
                    return null;

                if (KeyEquals(b.key, key))
                    return b.value;

                collisionCount++;
                bucketNumber = (int)(seed + collisionCount * incr) % _buckets.Length;
            }
            while (++ntry < _buckets.Length);
            return null;
        }
        set => Insert(key, value, false);
    }
    public virtual void Add(object key, object? value)
    {
        Insert(key, value, true);
    }
    public virtual void Remove(object key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key), "Key must not be null");

        uint hashcode = InitHash(key, _buckets.Length, out uint seed, out uint incr);
        int ntry = 0, collisionCount = 0;
        int bucketNumber = (int)seed;
        bucket b;
        do
        {
            b = _buckets[bucketNumber];
            if(KeyEquals(b.key, key))
            {
                b.key = null;
                b.value = null;
                _count--;
                return;
            }

            collisionCount++;
            bucketNumber = (int)(seed + collisionCount * incr) % _buckets.Length;
        }
        while (++ntry < _buckets.Length);
    }
    public virtual bool Contains(object key)
    {
        return ContainsKey(key);
    }
    public virtual bool ContainsKey(object key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key), "key must not be null");

        uint hashcode = InitHash(key, _buckets.Length, out uint seed, out uint incr);
        int ntry = 0, collisionCount = 0;
        int bucketNumber = (int)seed;

        bucket b;

        do
        {
            b = _buckets[bucketNumber];

            if (b.key == null)
                return false;

            if (KeyEquals(b.key, key))
                return true;

            collisionCount++;
            bucketNumber = (int)(bucketNumber + collisionCount * incr) % _buckets.Length;
        }
        while(++ntry < _buckets.Length);

        return false;
    }
    public virtual bool ContainsValue(object? value)
    {
        if(value is null)
        {
            for(int i = 0; i < _buckets.Length; ++i)
            {
                if (_buckets[i].key != null && _buckets[i].value == null)
                    return true;
            }
        }
        else
        {
            for(int i = 0; i <= _buckets.Length; ++i)
            {
                object? val = _buckets[i].value;
                if (val != null && val.Equals(val))
                    return true;
            }
        }

        return false;
    }
    public virtual void Clear()
    {
        if (_count == 0)
            return;

        for(int i = 0; i < _buckets.Length; ++i)
        {
            _buckets[i].key = null;
            _buckets[i].value = null;
        }

        _count = 0;
    }


    private void Expand()
    {
        int newSize = GetLargerPrime(_buckets.Length);
        rehash(newSize);
    }
    private void rehash(int newsize)
    {
        bucket[] newBuckets = new bucket[newsize];
        int nb;
        for (nb = 0; nb < _buckets.Length; nb++)
        {
            bucket oldb = _buckets[nb];
            if (oldb.key != null)
                PutEntry(newBuckets, oldb.key, oldb.value);
        }

        _buckets = newBuckets;
        _loadsize = (int)(_loadFactor * newsize);
    }
    private void PutEntry(bucket[] newBuckets, object key, object? nvalue)
    {
        uint hashcode = InitHash(key, _buckets.Length, out uint seed, out uint incr);
        int collisionCount = 0;
        int bucketNumber = (int)seed;

        while(true)
        {
            if (newBuckets[bucketNumber].value == null)
            {
                newBuckets[bucketNumber].value = nvalue;
                newBuckets[bucketNumber].key = key;
                return;
            }

            collisionCount++;
            bucketNumber = (int)(seed + collisionCount * incr) % _buckets.Length;
        }
    }

    private int GetPrime(int rawsize)
    {
        for(int i = rawsize - 1; i > 3; i--)
        {
            if (IsPrime(i)) return i;
        }
        return 3;
    }
    private int GetSmallerPrime(int number)
    {
        for(int i = number - 1; i > 1; i--)
        {
            if (IsPrime(i)) return i;
        }
        return 1;
    }
    private int GetLargerPrime(int number)
    {
        for (int i = number + 1; true; i++)
        {
            if (IsPrime(i)) return i;
        }
    }
    private bool IsPrime(int number)
    {
        for(int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }


    // Double hashing
    private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
    {
        // seed is h1(key), it is the index of bucket
        // incre is h2(key, hashsize), it is the step size of sequence probes

        // Index = seed % bucketsize
        // Incr = 1 + (seed % (hashSize-1))
        // H(key, i) = h1(key) + i*h2(key, hashSize).

        uint hashcode = (uint)GetHash(key) & 0x7FFFFFFF;
        seed = hashcode % (uint)hashsize; // This is h1(key) = k % m
        int primeNumber = GetSmallerPrime(hashsize);
        incr = hashcode % (uint)primeNumber; // This is h2(k) = k % p
        return hashcode;
    }
    protected virtual int GetHash(object key)
    {
        return key.GetHashCode();
    }


    private void Insert(object key, object? nvalue, bool add)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key), "Key must not be null");

        if (_count >= _loadsize)
            Expand();

        uint hashcode = InitHash(key, _buckets.Length, out uint seed, out uint incr);
        int ntry = 0, collisionCount = 0;
        int bucketNumber = (int)seed;
        do
        {
            // This bucket is available to use
            if (_buckets[bucketNumber].key == null)
            {
                _buckets[bucketNumber].value = nvalue;
                _buckets[bucketNumber].key = key;
                _count++;
                return;
            }

            // This bucket is in use that has the same key, so update it
            if (KeyEquals(_buckets[bucketNumber].key, key))
            {
                if (add)
                    throw new ArgumentException("Key is already existed");
                _buckets[bucketNumber].value = nvalue;
                return;
            }

            // There is a collision, so move to the next step
            collisionCount++;
            bucketNumber = (int)(seed + collisionCount * incr) % _buckets.Length;
        }
        while (++ntry < _buckets.Length);
    }
    protected virtual bool KeyEquals(object? item, object key)
    {
        if (object.ReferenceEquals(_buckets, item))
            return false;

        if (object.ReferenceEquals(item, key))
            return true;

        return item == null ? false : item.Equals(key);
    }
}
