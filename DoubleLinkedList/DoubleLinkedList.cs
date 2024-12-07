using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedList;

internal class DoubleLinkedList
{
    public Node? Head { get; set; }
    public Node? Tail { get; set; }

    public Node InitializeDoubleLinkedList(int data)
    {
        if (Head != null)
            return Head;

        Head = new Node(data);
        Tail = Head;
        return Head;
    }

    public int FindLength()
    {
        int length = 0;
        var currentNode = Head;
        while (currentNode != null)
        {
            length++;
            currentNode = currentNode.Next;
        }
        return length;
    }

    public void TraverseDoubleLinkedList()
    {
        var currentNode = Head;
        while (currentNode != null)
        {
            System.Console.Write($"{currentNode.Data} ");
            currentNode = currentNode.Next;
        }
    }

    public Node InsertAtBeginning(int data)
    {
        Node headNode = new Node(data);

        headNode.Next = Head;
        if(Head != null)
        {
            Head.Prev = headNode;
        }

        Head = headNode;
        if(Tail == null)
        {
            Tail = Head;
        }

        return Head;
    }

    public Node InsertAtEnd(int data)
    {
        Node endNode = new Node(data);

        if (Head == null || Tail == null)
        {
            Head = Tail = endNode;
            return Head;
        }

        Tail.Next = endNode;
        endNode.Prev = Tail;
        Tail = endNode;

        return Head;
    }

    // Position start from 0
    public Node? FindNodeAtPosition(int position)
    {
        if(Head is null || Tail is null)
            return null;

        int count = 0;
        Node? node = Head;
        while (count < position && node != null)
        {
            count++;
            node = node.Next;
        }

        return node;
    }

    public Node? InsertBeforeANode(Node node, int data)
    {
        Node newNode = new Node(data);

        Node? prevNode = node.Prev;
        if(prevNode != null)
        {
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
        }

        newNode.Next = node;
        node.Prev = newNode;

        return Head;
    }

    public Node? InsertAfterANode(Node node, int data)
    {
        Node newNode = new Node(data);

        Node? nextNode = node.Next;
        if (nextNode != null)
        {
            nextNode.Prev = newNode;
            newNode.Next = nextNode;
        }

        newNode.Prev = node;
        node.Next = newNode;

        return Head;
    }

    public Node? ReverseUsingHead()
    {
        if (Head is null || Tail is null)
            return null;

        // Set tail
        Tail = Head;

        // Set head
        var nextNode = Head.Next;
        Head.Next = null;
        Head.Prev = nextNode;

        while (true)
        {
            // If move head forward, need to make sure it is not null
            if (nextNode is null) break;

            // Set head to move forward to tail step by step in each loop (tail -> head)
            var tempNode = Head;
            Head = nextNode;
            nextNode = nextNode.Next;

            Head.Next = tempNode;
            Head.Prev = nextNode;
        }

        return Head;
    }

    public Node? ReverseUsingTail()
    {
        if (Head is null || Tail is null)
            return null;

        // Set Head
        Head = Tail;

        // Set Tail
        var prevNode = Tail.Prev;
        Tail.Prev = null;
        Tail.Next = prevNode;

        while (true)
        {
            if(prevNode is null) break;

            // Mỗi vòng lập sẽ di chuyển tail lên đuôi mới là head (head -> tail)
            // Tail thành head -> ưu tiên next tiến lên
            // TempNode sẽ là prev của node đang lên đầu
            var tempNode = Tail;
            Tail = prevNode;
            prevNode = prevNode.Prev;

            // Set Next và Prev cho tail mới
            Tail.Next = prevNode;
            Tail.Prev = tempNode;
        }

        return Head;
    }
}
