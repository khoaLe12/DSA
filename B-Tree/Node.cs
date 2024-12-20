using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree;

internal class Node
{
    public int[] Keys { get; set; }
    public Node?[] Child { get; set; }
    public int Count { get; private set; } // Count of keys, count of children = count of keys + 1
    public bool IsLeaf { get; private set; }

    public Node(int order = 5, bool isLeaf = true)
    {
        Keys = new int[order-1];
        Child = new Node[order];
        Count = 0;
        IsLeaf = isLeaf;
    }

    public void Traverse()
    {
        // InorderTraversal: Print left nodes -> current node -> last right node
        int i = 0;
        for( ; i < Count; i++)
        {
            if (!this.IsLeaf)
            {
                // Print all left child first
                Child[i]?.Traverse();
            }
            // Print current node
            Console.Write($" {Keys[i]}");
        }
        Console.Write(", ");
        // Print the last right child
        Child[i]?.Traverse();
    }

    public Node? Search(int data)
    {
        int i = 0;
        while(i < Count && data > this.Keys[i])
        {
            i++;
        }

        if (i < Count && this.Keys[i] == data) return this;

        if(this.IsLeaf) return null;

        return this.Child[i]?.Search(data);
    }

    public bool InsertKey(int? data)
    {
        if (data is null) return false;

        if (Count == Keys.Length)
            return false;
        int i = 0;
        for(; i < Count; i++)
        {
            if (data == this.Keys[i]) return false;
            if(data < Keys[i])
            {
                int temp = data.Value;
                data = Keys[i];
                Keys[i] = temp;
            }
        }
        Keys[i] = data.Value;
        Count++;
        return true;
    }

    public int? SplitChildWithKey(Node child, int? key)
    {
        if (key is null) return null;

        int indexOfChild = -1, i = 0;
        for (; i < this.Child.Length; i++)
        {
            if(object.ReferenceEquals(child, this.Child[i]))
            {
                indexOfChild = i;
                break;
            }
        }
        if (indexOfChild == -1) return null;

        // Split the child
        int[] keys = new int[child.Count + 1];
        for (i = 0; i < child.Count; i++)
        {
            if (key < child.Keys[i])
            {
                keys[i] = key.Value;
                key = child.Keys[i];
            }
            else keys[i] = child.Keys[i];
        }
        keys[i] = key.Value;
        var middleKey = keys[keys.Length/2];
        

        if(this.Count == this.Keys.Length) 
            return middleKey;

        this.InsertKey(middleKey);
        return null;
    }
}
