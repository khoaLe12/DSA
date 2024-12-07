using System.Drawing;
using System.Runtime.InteropServices;

namespace HashTable;

internal class Array<T> where T : unmanaged
{
    private IntPtr _pointer;
    private int _capacity;
    private int _count;

    public Array(int capacity = 5)
    {
        if(capacity < 0)
            throw new ArgumentException("Capacity must be greater than zero.");

        _capacity = capacity;
        _count = 0;
        _pointer = Marshal.AllocHGlobal(Marshal.SizeOf<T>() * capacity);
    }

    public T this[int index]
    {
        get
        {
            if(index < 0 || index >= _capacity)
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }
            return Marshal.PtrToStructure<T>(_pointer + index * Marshal.SizeOf<T>());
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Index out of range.");

            Marshal.StructureToPtr(value, _pointer + index * Marshal.SizeOf<T>(), false);
        }
    }

    public void Add(T item)
    {
        if(_count == _capacity)
        {
            Resize();
        }

        Marshal.StructureToPtr(item, _pointer + _count * Marshal.SizeOf<T>(), false);
        _count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException("Index out of range.");

        // Shift the elements to the left
        for(int i = index; i < _count - 1; i++)
        {
            var nextElementValue = Marshal.PtrToStructure<T>(_pointer + (i + 1) * Marshal.SizeOf<T>());
            Marshal.StructureToPtr(nextElementValue, _pointer + i * Marshal.SizeOf<T>(), false);
        }

        _count--;
        Marshal.StructureToPtr(default(T), _pointer + _count * Marshal.SizeOf<T>(), true); // Clear last element
    }

    private void Resize()
    {
        int newCapacity = _capacity * 2;
        IntPtr newPointer = Marshal.AllocHGlobal(Marshal.SizeOf<T>() * newCapacity);

        for(int i = 0; i < _count; i++)
        {
            var elementValue = Marshal.PtrToStructure<T>(_pointer + i * Marshal.SizeOf<T>());
            Marshal.StructureToPtr(elementValue, newPointer + i * Marshal.SizeOf<T>(), false);
        }

        Marshal.FreeHGlobal(_pointer);
        _pointer = newPointer;
        _capacity = newCapacity;
    }

    public void Dispose()
    {
        if(_pointer != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_pointer);
            _pointer = IntPtr.Zero;
            _capacity = 0;
            _count = 0;
        }
    }

    ~Array()
    {
        Dispose();
    }
}
