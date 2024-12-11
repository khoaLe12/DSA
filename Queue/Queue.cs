using LinkedList;
using LinkeddList = LinkedList.LinkedList;

namespace Queue;

public class Queue
{
    private LinkeddList List { get; set; } = new LinkeddList();

    public void Enqueue(int data)
    {
        List.InsertAtEnd(data);
    }

    public Node? Dequeue()
    {
        var node = List.SearchByPosition(0);
        List.DeleteAtBeginning();
        return node;
    }

    public Node? Peek()
    {
        return List.SearchByPosition(0);
    }

    public int Count()
    {
        return List.FindLength();
    }

    public void Clear()
    {
        List.Clean();
    }

    public bool Contains(int data)
    {
        var node = List.SearchByValue(data);
        return node != null;
    }
}
