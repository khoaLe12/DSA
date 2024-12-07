// See https://aka.ms/new-console-template for more information
using DoubleLinkeddList = DoubleLinkedList.DoubleLinkedList;


DoubleLinkeddList list = new DoubleLinkeddList();
list.InsertAtEnd(1);
list.InsertAtEnd(2);
list.InsertAtEnd(3);
list.InsertAtEnd(4);
list.InsertAtEnd(5);
list.InsertAtEnd(6);
list.InsertAtEnd(7);
list.InsertAtEnd(8);
list.InsertAtBeginning(10);


Console.Write("Traverse list: ");
list.TraverseDoubleLinkedList();


var searchNode = list.FindNodeAtPosition(3);
Console.Write($"\n\nFind node at position 3: {searchNode?.Data}");
if (searchNode is null) return;


list.InsertBeforeANode(searchNode, 11);
Console.Write("\n\nInsert before searched node: ");
list.TraverseDoubleLinkedList();


list.InsertAfterANode(searchNode, 12);
Console.Write("\n\nInsert after searched node: ");
list.TraverseDoubleLinkedList();


list.ReverseUsingHead();
Console.Write("\n\nReverse list using Head: ");
list.TraverseDoubleLinkedList();


list.ReverseUsingTail();
Console.Write("\n\nReverse list using Tail: ");
list.TraverseDoubleLinkedList();