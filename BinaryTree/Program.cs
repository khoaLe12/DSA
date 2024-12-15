using BinaryTreee = BinaryTree.BinaryTree;
using BinaryTree;


//DemoBinaryTree();

DemoBinarySearchTree();




void DemoBinaryTree()
{
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

    //Demo1(tree);

    Console.Write($"\n\nFind height using BFS: {tree.GetHeightBFS()}");
    Console.Write($"\n\nFind height using DFS: {tree.GetHeightDFS()}");

    var searchNode12 = tree.SearchBFS(12);
    if (searchNode12 is null)
        Console.Write("\n\nNode with data 12 not found");
    else
    {
        var ancestor = tree.FindAncestor(searchNode12);
        Console.Write($"\n\nAncestor of node 12 is: {ancestor?.Data}");

        var descendants = tree.FindDescendants(searchNode12);
        Console.Write($"\n\nDescendants of node 12 is: ");
        foreach (var node in descendants)
        {
            Console.Write($"{node.Data} ");
        }
    }
}
void Demo1(BinaryTreee tree)
{
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
}


void DemoBinarySearchTree()
{
    var root = new Node(10);

    BinarySearchTree tree = new(root);
    tree.Insert(3);
    tree.Insert(5);
    tree.Insert(1);
    tree.Insert(16);
    tree.Insert(20);
    tree.Insert(14);
    tree.Insert(25);
    tree.Insert(9);
    // Tree
    // Lv0                10
    // Lv1        3                  16
    // Lv2      1   5             14    20
    // Lv3            9                   25


    Console.Write("Traverse the tree: ");
    tree.InorderTraversal();


    var node10 = tree.Search(10);
    var node14 = tree.Search(14);
    var node9 = tree.Search(9);
    var node5 = tree.Search(5);
    Console.WriteLine($"\n\nNode 10: {node10?.Data}");
    Console.WriteLine($"Node 14: {node14?.Data}");
    Console.WriteLine($"Node 9: {node9?.Data}");
    Console.Write($"Node 5: {node5?.Data}");

    //DemoDeleteInBST(tree);


    Console.WriteLine($"\n\nThe minimum value of tree is: {tree.Minimum()}");
    Console.Write($"The maximum value of tree is: {tree.Maximum()}");


    Console.WriteLine($"\n\nThe floor of 15 is: {tree.Floor(15)}"); // Expected 14
    Console.WriteLine($"The floor of 14 is: {tree.Floor(14)}"); //  Expected 14
    Console.Write($"The floor of 13 is: {tree.Floor(13)}"); // Expected 10


    Console.WriteLine($"\n\nThe ceil of 13 is: {tree.Ceil(13)}"); // Expected 14
    Console.WriteLine($"The ceil of 6 is: {tree.Ceil(6)}"); //  Expected 9
    Console.WriteLine($"The ceil of 2 is: {tree.Ceil(2)}"); // Expected 3
    Console.Write($"The ceil of 3 is: {tree.Ceil(3)}"); // Expected 3


    //DemoAddAllInBST(tree);

    //DemoConvertToBalancedBST(tree);

    DemoGetMaximumElementBetweenNodes(tree);
}

void DemoDeleteInBST(BinarySearchTree tree)
{
    tree.Delete(10); // Expect 10 is replaced by 14
    Console.Write("\n\nTraverse the tree: ");
    tree.InorderTraversal();

    tree.Delete(20); // Expect 20 is replaced by 25
    Console.Write("\n\nTraverse the tree: ");
    tree.InorderTraversal();

    tree.Delete(25); // Expect 25 is deleted
    Console.Write("\n\nTraverse the tree: ");
    tree.InorderTraversal();
}

void DemoAddAllInBST(BinarySearchTree tree)
{
    tree.Insert(15);
    tree.Insert(21);
    tree.Insert(6);
    tree.AddAllGreaterValue();
    Console.Write("\n\nTraverse the tree: ");
    tree.InorderTraversal();

    // Before Add all
    // Tree
    // Lv0                10
    // Lv1        3                  16
    // Lv2      1   5            14      20
    // Lv3            9            15      25
    // Lv4          6                    21

    // After Add all
    // Tree
    // Lv0                         121
    // Lv1         144                                82
    // Lv2    145      141                     111           66
    // Lv3                 130                    97               25
    // Lv4             136                                       46 
}

void DemoConvertToBalancedBST(BinarySearchTree tree)
{
    tree.Insert(26);
    tree.Insert(28);
    tree.Insert(27);
    // Tree
    // Lv0                10
    // Lv1        3                  16
    // Lv2      1   5             14    20
    // Lv3            9                   25
    // Lv4                                  26
    // Lv5                                    28
    // Lv6                                  27

    Console.Write("\n\nBefore convert to Balanced BST: ");
    tree.PrintNodeOnEachLevel();

    tree.ConvertToBalancedBST();

    Console.Write("\nAfter convert to Balanced BST: ");
    tree.PrintNodeOnEachLevel();
}

void DemoGetMaximumElementBetweenNodes(BinarySearchTree tree)
{
    tree.Insert(26);
    tree.Insert(28);
    tree.Insert(27);
    // Tree
    // Lv0                10
    // Lv1        3                  16
    // Lv2      1   5             14    20
    // Lv3            9                   25
    // Lv4                                  26
    // Lv5                                    28
    // Lv6                                  27

    Console.Write($"\n\nThe maximum element between 2 nodes '9' and '27' is: {tree.GetMaximumElementBetweenNodes(9, 27)}");
    Console.Write($"\nThe maximum element between 2 nodes '5' and '20' is: {tree.GetMaximumElementBetweenNodes(5, 20)}");
    Console.Write($"\nThe maximum element between 2 nodes '9' and '10' is: {tree.GetMaximumElementBetweenNodes(9, 10)}");
    Console.Write($"\nThe maximum element between 2 nodes '10' and '9' is: {tree.GetMaximumElementBetweenNodes(10, 9)}");
    Console.Write($"\nThe maximum element between 2 nodes '10' and '27' is: {tree.GetMaximumElementBetweenNodes(10, 27)}");
}