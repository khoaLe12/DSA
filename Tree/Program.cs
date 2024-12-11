using BinaryTreee = BinaryTree.BinaryTree;
using BinaryTree;

var root = new Node(10);
// Lv1
root.LeftNode = new Node(1);
root.RightNode = new Node(3);
// Lv2
root.LeftNode.LeftNode = new Node(5);
root.LeftNode.RightNode = new Node(7);
root.RightNode.LeftNode = new Node(9);
// Lv3
root.LeftNode.LeftNode.RightNode = new Node(11);
// Lv4
root.LeftNode.LeftNode.RightNode.LeftNode = new Node(12);
root.LeftNode.LeftNode.RightNode.RightNode = new Node(6);

// Tree
// Lv0                        10(x1)
// Lv1             1                      3
// Lv2       5(x2)       7           9         x1
// Lv3    x2    11(x3)
// Lv4       12     6
// Lv5    x3

BinaryTreee tree = new BinaryTreee(root);

Console.Write("Pre Order DFS: ");
tree.PreorderDFS();

Console.Write("\n\nIn Order DFS: ");
tree.InorderDFS();

Console.Write("\n\nPost Order DFS: ");
tree.PostorderDFS();

Console.Write("\n\nBFS: ");
tree.BFS();

Console.WriteLine($"\n\nSearch DFS 12: {tree.SearchDFS(12) is not null}");
Console.WriteLine($"Search BFS 12: {tree.SearchBFS(12) is not null}");

Console.WriteLine($"Search DFS 9: {tree.SearchDFS(9) is not null}");
Console.WriteLine($"Search BFS 9: {tree.SearchBFS(9) is not null}");

Console.WriteLine($"Search DFS 7: {tree.SearchDFS(7) is not null}");
Console.WriteLine($"Search BFS 7: {tree.SearchBFS(7) is not null}");

Console.WriteLine($"Search DFS 4: {tree.SearchDFS(4) is not null}");
Console.WriteLine($"Search BFS 4: {tree.SearchBFS(4) is not null}");


var searchNode10 = tree.SearchDFS(10);
if (searchNode10 is null)
    Console.Write("\n\nNode with data 10 not found");
else
{
    tree.InsertAtNode(searchNode10, 100);
    Console.Write("\n\nAfter inserting 100 at node 10, traversal BFS: ");
    tree.BFS();
}

var searchNode5 = tree.SearchBFS(5);
if (searchNode5 is null)
    Console.Write("\n\nNode with data 5 not found");
else
{
    tree.InsertAtNode(searchNode5, 50);
    Console.Write("\n\nAfter inserting 50 at node 5, traversal BFS: ");
    tree.BFS();
}


var searchNode11 = tree.SearchBFS(11);
if (searchNode11 is null)
    Console.Write("\n\nNode with data 11 not found");
else
{
    tree.InsertAtNode(searchNode11, 110);
    Console.Write("\n\nAfter inserting 110 at node 11, traversal BFS: ");
    tree.BFS();
}


tree.DeleteNode(5);
Console.Write("\n\nAfter deleting node with value 5: ");
tree.BFS();

tree.DeleteNode(10);
Console.Write("\n\nAfter deleting node with value 10: ");
tree.BFS();