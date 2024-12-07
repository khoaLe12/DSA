using LinkedList;
using LinkeddList = LinkedList.LinkedList;

namespace Queue;

internal class Queue
{
    private LinkeddList List { get; set; } = new LinkeddList();

    internal void Enqueue(int data)
    {
        List.InsertAtEnd(data);
    }

    internal Node? Dequeue()
    {
        var node = List.SearchByPosition(0);
        List.DeleteAtBeginning();
        return node;
    }

    internal Node? Peek()
    {
        return List.SearchByPosition(0);
    }

    internal int Count()
    {
        return List.FindLength();
    }

    internal void Clear()
    {
        List.Clean();
    }

    internal bool Contains(int data)
    {
        var node = List.SearchByValue(data);
        return node != null;
    }
}
