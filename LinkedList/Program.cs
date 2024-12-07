// See https://aka.ms/new-console-template for more information
using LinkeddList = LinkedList.LinkedList;

Console.WriteLine("Hello, World!");

var linkedList = new LinkeddList();
linkedList.InsertAtEnd(0);
linkedList.InsertAtEnd(1);
linkedList.InsertAtEnd(2);
linkedList.InsertAtEnd(3);
linkedList.InsertAtEnd(4);
linkedList.InsertAtEnd(4);
linkedList.InsertAtEnd(5);
linkedList.InsertAtEnd(6);
linkedList.InsertAtEnd(7);
linkedList.InsertAtEnd(8);
// 9 số


Console.WriteLine($"Length: {linkedList.FindLength()}");
linkedList.TraverseLinkedList();


Console.Write("\n\nReverse List: ");
linkedList.Reverse();
linkedList.TraverseLinkedList();


Console.Write("\n\nOrder by ascending: ");
linkedList.SortByAscend();
linkedList.TraverseLinkedList();


Console.Write("\n\nInsert at specific position: ");
linkedList.InsertAtSpecificPosition(10, 5);
linkedList.TraverseLinkedList();

Console.Write("\n\nDelete by value 10: ");
linkedList.DeleteByValue(10);
linkedList.TraverseLinkedList();


Console.Write("\n\nDelete at beginning and end: ");
linkedList.DeleteAtBeginning();
linkedList.DeleteAtEnd();
linkedList.TraverseLinkedList();


Console.Write("\n\nDelete at position 3: ");
linkedList.DeleteAtASpecificPosition(3);
linkedList.TraverseLinkedList();


Console.Write("\n\nFind middle node: ");
var middleNode = linkedList.FindMiddleNode();
Console.Write(middleNode?.Data);


Console.Write("\n\nFind 3th node from end: ");
var nodeFromEnd = linkedList.FindNthNodeFromEnd(3);
Console.Write(nodeFromEnd?.Data);

