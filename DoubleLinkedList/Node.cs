using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedList;

internal class Node
{
    public int Data { get; set; }
    public Node? Prev { get; set; } = null;
    public Node? Next { get; set; } = null;

    public Node(int data)
    {
        Data = data;
    }
}
