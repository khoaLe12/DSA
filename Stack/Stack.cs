using LinkedList;
using LinkeddList = LinkedList.LinkedList;

namespace Stack;

internal class Stack
{
    private LinkeddList List { get; set; } = new LinkeddList();

    internal void Push(int data)
    {
        List.InsertAtEnd(data);
    }

    internal Node? Pop()
    {
        var node = List.SearchAtEnd();
        List.DeleteAtEnd();
        return node;
    }

    internal Node? Peek()
    {
        return List.SearchAtEnd();
    }

    internal int Count()
    {
        return List.FindLength();
    }

    internal void Clear()
    {
        List.Clean();
        // Or List.FreeAll();
    }

    internal bool Contains(int data)
    {
        var searchNode = List.SearchByValue(data);
        return searchNode != null;
    }
}
