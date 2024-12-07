using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularLinkedList;

internal class CircularLinkedList
{
    public Node? Head { get; set; } = null;

    public Node Initilize(int data)
    {
        Node newNode = new Node(data, null);
        Head = newNode;
        Head.Next = Head;
        return Head;
    }

    public void TraverseList()
    {
        if (Head is null) return;
        var temp = Head;
        do
        {
            Console.Write($"{temp?.Data} ");
            temp = temp?.Next;
        } while (temp != null && temp != Head);
    }

    public Node? InsertAtEnd(int data)
    {
        if(Head is null)
            return Initilize(data);

        var temp = Head;
        while(temp.Next != null && temp.Next != Head)
        {
            temp = temp.Next;
        }

        if (temp is null) return Head;

        var newNode = new Node(data, Head);
        temp.Next = newNode;

        return Head;
    }

    public Node? SearchByData(int data)
    {
        if (Head is null) return null;

        var temp = Head;
        while (temp != null && temp.Data != data)
        {
            temp = temp.Next;
            if (temp == Head) return null;
        }

        return temp;
    }
}
