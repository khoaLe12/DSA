using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree;

internal class Node
{
    public int Data { get; set; }
    public Node? LeftNode { get; set; }
    public Node? RightNode { get; set; }

    public Node(int data)
    {
        Data = data;
    }
}
