// See https://aka.ms/new-console-template for more information

using CircularLinkeddList = CircularLinkedList.CircularLinkedList;


var circularLinkedList = new CircularLinkeddList();

circularLinkedList.InsertAtEnd(0);
circularLinkedList.InsertAtEnd(1);
circularLinkedList.InsertAtEnd(2);
circularLinkedList.InsertAtEnd(3);
circularLinkedList.InsertAtEnd(4);
circularLinkedList.InsertAtEnd(5);
circularLinkedList.InsertAtEnd(6);
circularLinkedList.InsertAtEnd(7);


Console.Write("Traverse list: ");
circularLinkedList.TraverseList();

Console.Write("\n\nSearch number 4: ");
var searchNode = circularLinkedList.SearchByData(4);
Console.Write(searchNode?.Data);