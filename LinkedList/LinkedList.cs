using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList;

public class LinkedList
{
    public Node? Head { get; set; } = null;

    public Node InitializeLinkedList(int data)
    {
        if(Head != null)
            return Head;

        Head = new Node(data, null);
        return Head;
    }

    public int FindLength()
    {
        int length = 0;
        var currentNode = Head;
        while(currentNode != null)
        {
            length++;
            currentNode = currentNode.Next;
        }
        return length;
    }

    public void TraverseLinkedList()
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
        Node headNode = new Node(data, Head);
        Head = headNode;
        return Head;
    }

    public Node InsertAtEnd(int data)
    {
        Node endNode = new Node(data, null);

        if(Head == null)
        {
            Head = endNode;
            return Head;
        }

        var currentEndNode = Head;
        while(currentEndNode.Next != null)
        {
            currentEndNode = currentEndNode.Next;
        }

        currentEndNode.Next = endNode;
        return Head;
    }

    // Position starts from 0
    public Node? InsertAtSpecificPosition(int data, int position)
    {
        if(position < 0)
        {
            System.Console.WriteLine("Invalid position");
            return null;
        }

        if(position == 0)
        {
            var newHeadNode = new Node(data, Head);
            Head = newHeadNode;
            return Head;
        }

        Node? prevNode = Head;
        int count = 0;
        while (count < position - 1 && prevNode != null)
        {
            count++;
            prevNode = prevNode.Next;
        }

        if(prevNode is null)
        {
            System.Console.WriteLine("Invalid position, the positioned node is null");
            return null;
        }

        var newNode = new Node(data, null);
        newNode.Next = prevNode.Next;
        prevNode.Next = newNode;

        return Head;
    }

    public Node? DeleteByValue(int data)
    {
        if(Head == null)
            return null;

        var prevNode = Head;
        while (prevNode.Next != null && prevNode.Next.Data != data)
            prevNode = prevNode.Next;

        if (prevNode is null || prevNode.Next is null)
            return null;

        var deleteNode = prevNode.Next;
        prevNode.Next = deleteNode.Next;
        deleteNode = null;

        return Head;
    }

    public Node? DeleteAtBeginning()
    {
        if(Head is null)
            return null;

        var newHeadNode = Head.Next;
        Head = newHeadNode;
        return Head;
    }

    public Node? DeleteAtEnd()
    {
        if (Head is null || Head.Next is null)
        {
            Head = null;
            return Head;
        }

        var prevNode = Head;
        while(prevNode.Next?.Next != null)
            prevNode = prevNode.Next;

        prevNode.Next = null;
        return Head;
    }

    // Position starts from 0
    public Node? DeleteAtASpecificPosition(int position)
    {
        if(position < 0)
        {
            Console.WriteLine("Invalid position");
        }

        if (Head is null)
            return null;

        if(position == 0)
        {
            var temp = Head.Next;
            Head = Head.Next;
            temp = null;
            return Head;
        }

        Node? prevNode = Head;
        int count = 0;
        while(count < position - 1 && prevNode != null)
        {
            count++;
            prevNode = prevNode.Next;
        }

        if(prevNode is null)
        {
            Console.WriteLine("Invalid position");
            return Head;
        }

        var deleteNode = prevNode.Next;
        prevNode.Next = prevNode.Next?.Next;
        deleteNode = null;

        return Head;
    }

    public Node? SearchByValue(int data)
    {
        if (Head is null)
            return null;

        var currentNode = Head;
        while(currentNode != null && currentNode.Data != data)
        {
            currentNode = currentNode.Next;
        }

        return currentNode;
    }

    // Position starts from 0
    public Node? SearchByPosition(int position)
    {
        if (Head is null)
            return null;

        int count = 0;
        var currentNode = Head;
        while (count < position && currentNode != null)
        {
            count++;
            currentNode = currentNode.Next;
        }

        return currentNode;
    }

    public Node? SearchAtEnd()
    {
        if (Head is null) return null;

        var temp = Head;
        while(temp.Next != null)
        {
            temp = temp.Next;
        }
        return temp;
    }

    public Node? UpdateAtPosition(int position, int data)
    {
        if (Head is null)
            return null;

        int count = 0;
        var currentNode = Head;
        while (count < position && currentNode != null)
        {
            count++;
            currentNode = currentNode.Next;
        }

        if (currentNode is null)
            return null;

        currentNode.Data = data;
        return currentNode;
    }

    public bool IsEmpty()
    {
        return Head is null;
    }

    public void Clean()
    {
        Head = null;
    }

    public void FreeAll()
    {
        while(Head != null)
        {
            var temp = Head;
            Head = Head.Next;
            temp = null;
        }
    }

    public Node? Reverse()
    {
        if (Head is null)
            return null;

        var currentNode = Head;
        var nextNode = Head.Next;

        currentNode.Next = null;

        while (nextNode != null)
        {
            var tempNode = nextNode.Next;
            if (tempNode is null)
                Head = nextNode;

            nextNode.Next = currentNode;
            currentNode = nextNode;
            nextNode = tempNode;
        }

        return Head;
    }

    public Node? FindMiddleNode()
    {
        if (Head is null)
            return null;

        if (Head.Next is null)
            return Head;

        int count = 1;
        Node? temp = Head;

        int position = 1;
        Node? middleNode = Head;

        while (temp != null)
        {
            count++;
            temp = temp.Next;

            if (count > position*2)
            {
                position++;
                middleNode = middleNode?.Next;
            }
        }

        return middleNode;
    }

    public Node? MergeAtBeginning(Node mergedHeadNode)
    {
        if(Head is null)
        {
            Head = mergedHeadNode;
            return Head;
        }

        var mergedEndNode = mergedHeadNode;
        while(mergedEndNode.Next != null)
        {
            mergedEndNode = mergedEndNode.Next;
        }

        mergedEndNode.Next = Head;
        Head = mergedHeadNode;

        return Head;
    }

    public Node? MergeAtEnd(Node mergedHeadNode)
    {
        if(Head is null)
        {
            Head = mergedHeadNode;
            return Head;
        }

        var endNode = Head;
        while(endNode.Next != null)
        {
            endNode = endNode.Next;
        }

        endNode.Next = mergedHeadNode;
        return Head;
    }

    // Position starts from 0
    public Node? FindNthNodeFromEnd(int position)
    {
        if (Head is null)
            return null;

        int count = 0;
        Node? temp = Head;

        int searchPosition = 0;
        Node? searchNode = Head;

        while(temp.Next != null)
        {
            count++;
            temp = temp.Next;

            if (count > position + searchPosition)
            {
                searchPosition++;
                searchNode = searchNode?.Next;
            }
        }

        if (count < position + searchPosition)
        {
            return null;
        }

        return searchNode;
    }

    public Node? SortByDescend()
    {
        return null;
    }

    public Node? SortByAscend()
    {
        Head = MergeSort(Head);
        return Head;
    }

    public Node? MergeSort(Node? head)
    {
        if (head is null)
            return null;
        if (head.Next is null)
            return head;

        // Identify the middle node
        var node2 = GetMiddleNode(head, out int position);
        var node1 = head;
        for(int i = 1; i < position - 1 && head != null; i++)
        {
            head = head?.Next;
        }
        if (head != null)
            head.Next = null;

        // Keep merge sort the above 2 lists
        var sortedNode1 = MergeSort(node1);
        var sortedNode2 = MergeSort(node2);

        return Merge(sortedNode1, sortedNode2);
    }

    public Node? Merge(Node? sortedNode1, Node? sortedNode2)
    {
        if (sortedNode1 is null) return sortedNode2;
        if (sortedNode2 is null) return sortedNode1;

        // Merge from the 2 lists that sort it
        Node? temp = null;
        if (sortedNode1.Data < sortedNode2.Data)
        {
            temp = sortedNode1;
            sortedNode1 = sortedNode1.Next;
        }
        else
        {
            temp = sortedNode2;
            sortedNode2 = sortedNode2.Next;
        }
        Node sortedList = temp;

        while (true)
        {
            if (sortedNode1 is null && sortedNode2 is null) break;
            if (sortedNode1 is null)
            {
                temp.Next = sortedNode2;
                break;
            }
            else if (sortedNode2 is null)
            {
                temp.Next = sortedNode1;
                break;
            }

            if (sortedNode1.Data < sortedNode2.Data)
            {
                temp.Next = sortedNode1;
                temp = temp.Next;
                sortedNode1 = sortedNode1.Next;
            }
            else
            {
                temp.Next = sortedNode2;
                temp = temp.Next;
                sortedNode2 = sortedNode2.Next;
            }
        }

        return sortedList;
    }

    public Node? GetMiddleNode(Node? header, out int position)
    {
        position = 0;
        if (header is null || header.Next is null)
            return null;

        int count = 1;
        Node? temp = header;

        Node? middleNode = header;
        position = 1;
        while (temp != null)
        {
            count++;
            temp = temp.Next;

            if (count >= position * 2)
            {
                position++;
                middleNode = middleNode?.Next;
            }
        }

        return middleNode;
    }
}