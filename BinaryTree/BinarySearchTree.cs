using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree;

internal class BinarySearchTree
{
    private Node? _root;

    public BinarySearchTree(Node root)
    {
        _root = root;
    }

    public void Insert(int data)
    {
        if(_root is null)
        {
            _root = new Node(data);
            return;
        }

        Node curr = _root;
        while (true)
        {
            if (data < curr.Data)
            {
                if (curr.LeftNode is not null)
                    curr = curr.LeftNode;
                else
                {
                    curr.LeftNode = new Node(data);
                    break;
                }
            }
            else if (data > curr.Data)
            {
                if (curr.RightNode is not null)
                    curr = curr.RightNode;
                else
                {
                    curr.RightNode = new Node(data);
                    break;
                }
            }
            else return;
        }
    }

    public Node? Search(int data)
    {
        if(_root is null) return null;

        var curr = _root;
        while(curr != null)
        {
            if(curr.Data == data) return curr;
            if(data < curr.Data) curr = curr.LeftNode;
            else curr = curr.RightNode;
        }

        return null;
    }

    public void Delete(int data)
    {
        if (_root is null) return;
        DeleteNode(_root, data);
    }
    private Node? DeleteNode(Node? root, int data)
    {
        if (root is null) return null;

        // This is for moving to the finding node
        if (data < root.Data)
            root.LeftNode = DeleteNode(root.LeftNode, data);
        else if (data > root.Data)
            root.RightNode = DeleteNode(root.RightNode, data);
        else
        {
            // The node is found here
            // If the node is a leaf or it has only 1 successor,
            // delete it by replacing it with its successor
            if(root.LeftNode is null) return root.RightNode;
            if (root.RightNode is null) return root.LeftNode;


            // If the delete node has 2 children, find in-order successor (The smallest node in the right subtree)
            var successor = GetSuccessor(root.RightNode);
            root.Data = successor.Data;

            // After replacing the value, need to delete the successor
            root.RightNode = DeleteNode(root.RightNode, successor.Data);
        }
        return root;
    }
    private Node GetSuccessor(Node curr)
    {
        while(curr != null && curr.LeftNode != null)
        {
            curr = curr.LeftNode;
        }
        return curr!;
    }

    public void InorderTraversal()
    {
        if(_root is null) return;
        InorderTraversal(_root);
    }
    private void InorderTraversal(Node? node)
    {
        if (node is null) return;
        InorderTraversal(node.LeftNode);
        Console.Write($"{node.Data} ");
        InorderTraversal(node.RightNode);
    }

    public int Minimum()
    {
        if (_root is null) return -1;
        var temp = _root;
        while (temp.LeftNode is not null)
            temp = temp.LeftNode;
        return temp.Data;
    }

    public int Maximum()
    {
        if (_root is null) return -1;
        var temp = _root;
        while (temp.RightNode is not null)
            temp = temp.RightNode;
        return temp.Data;
    }


    // Find the greatest value that smaller than data
    // Need to find the value on the 2 sides of a node (2 subtrees)
    // Start with the right side if data is greater than value of cur node, left side if its smaller
    // Some properties are critical:
    // 1. If the current node has value smaller than data, and the right side is null => the result is current node
    // 2. If the current node has value larger than data, and the left side is null => the result is null (from the current node to below, there is no appropriate node)
    // 3. If the current node has value smaller than data, and the right side is not null
    // (there could be a node that is greater than the current node and smaller than data, but it could also none)
    // => If it exist, return it. But if it is none (null), return the current node
    // Each recursive will find the result at a specific node (position)
    public int Floor(int data)
    {
        if (_root is null) return -1;
        var node = Floor(_root, data);
        if (node is null) return -1;
        return node.Data;
    }
    private Node? Floor(Node node, int data)
    {
        if (node.Data == data)
            return node;

        if (data > node.Data)
        {
            if (node.RightNode is null) return node;
            var result = Floor(node.RightNode, data);
            if (result is null) return node;
            return result;
        }
        else
        {
            if (node.LeftNode is null) return null;
            return Floor(node.LeftNode, data);
        }
    }


    // Find the smallest value that greater than data
    // Same as the above but opposite
    public int Ceil(int data)
    {
        if (_root is null) return -1;
        var node = Ceil(_root, data);
        if(node is null) return -1;
        return node.Data;
    }
    private Node? Ceil(Node node, int data)
    {
        if (node.Data == data) return node;

        if (node.Data > data) // Go to left to try to find whether if there is a smaller value 
        {
            if (node.LeftNode is null) return node;
            var result = Ceil(node.LeftNode, data);
            if(result is null) return node;
            return result;
        }
        else // Go to right to find node that greater than data
        {
            if(node.RightNode is null) return null;
            return Ceil(node.RightNode, data);
        }
    }



    // Easy problem
    // For each node in the tree, it will be added to values of nodes that greater than it self.
    // Illustration
    //                        Node 'A'
    //    Node 'B'                                Node 'C'
    //            Node 'D'
    // Some properties are recognized
    // 1. The most right node will not be added by any value (it is the largest vaue)
    // 2. The most left node will be added by all values (it is the smallest value)
    // 3. Each node's value is added by all values on the right side of it (Node A = Node A  + Node C)
    // 4. Left node is added by all values on the right side of it, its parent nodes, and all values on the right side of its parent node (Node 'B' = Node 'B'  +  Node 'D'  +  Node 'A'  +  Node 'C')
    //                                                                                                                                    Left node               Right side   Parent node  Right side of parent node
    // => Start from root and try to reach to the most right, and step by step reach to the most left
    // The calculation will start from the most right, try to sum all values on each step
    public void AddAllGreaterValue()
    {
        if (_root is null) return;
        AddValueOnEachNode(_root, 0);
    }
    private int AddValueOnEachNode(Node node, int parentValue)
    {
        // The idea is to keep the total of all values that greater than the current node
        // => Calculate the current node only by adding it with that total value
        //
        // parentValue here is the sum of all values that greater than the current node
        // totalRightValue also represent the sum of all values that greater than the current node (it is more prioritized than parentValue)
        // Calculate right -> current -> left
        //               10                                       10
        //          8                                    (8+10)=18
        //     5                       ->        (5+24)  
        //       6                                   (6+18)=24

        // Calculate value of the right side of the current node
        int totalRightValue = 0;
        if (node.RightNode is not null)
            totalRightValue = AddValueOnEachNode(node.RightNode, parentValue);

        // Calculate value of the current node
        if (totalRightValue > parentValue) 
            node.Data += totalRightValue;
        else 
            node.Data += parentValue;

        // Calculate value of the left side of the current node
        int totalLeftValue = 0;
        if(node.LeftNode is not null)
            totalLeftValue = AddValueOnEachNode(node.LeftNode, node.Data);

        if (totalLeftValue != 0) return totalLeftValue;
        return node.Data;
    }



    // Easy problem
    // Convert BST to balanced BST (the heights of the left and right subtrees differ by at most 1)
    public void ConvertToBalancedBST()
    {
        if (_root is null) return;
        // Get data in InorderTraversal 
        List<int> data = new List<int>();
        GetDataInorderTraversal(_root, ref data);
        _root = ConvertToBalancedBST(data, 0, data.Count() - 1);
    }
    private void GetDataInorderTraversal(Node? node, ref List<int> data)
    {
        if(node is null) return;
        GetDataInorderTraversal(node.LeftNode, ref data);
        data.Add(node.Data);
        GetDataInorderTraversal(node.RightNode, ref data);
    }
    private Node? ConvertToBalancedBST(List<int> data, int start, int end)
    {
        int middleIndex = (end - start) / 2;
        middleIndex += start;

        Node root = new Node(data.ElementAt(middleIndex));

        if(middleIndex != start)
            root.LeftNode = ConvertToBalancedBST(data, start, middleIndex - 1);
        if(middleIndex != end)
            root.RightNode = ConvertToBalancedBST(data, middleIndex + 1, end);

        return root;
    }
    public void PrintNodeOnEachLevel()
    {
        if (_root is null) return;

        int level = 0;
        Console.Write($"Lv{level} ");

        Queue<Node?> queue = new();
        queue.Enqueue(_root);
        queue.Enqueue(null);

        while (queue.Count > 0)
        {
            Node? node = queue.Dequeue();

            if (node is null)
            {
                if(queue.Count > 0)
                {
                    queue.Enqueue(null);
                    level++;
                    Console.Write($"   ,Lv{level} ");
                }
            }
            else
            {
                Console.Write($"{node.Data} ");
                if (node.LeftNode is not null) queue.Enqueue(node.LeftNode);
                if (node.RightNode is not null) queue.Enqueue(node.RightNode);
            }
        }
    }


    // Medium problem
    // Find the maximum element between two nodes of BST
    // The idea to to use recursion to travel from root to destination nodes (root is Lowest Common Ancestor)
    // The recursion will create a path from node1 to node2 passing through the root/LCA node
    // Once it reach to destination, start to get the maximum value, and step by step go back to the root
    // At each step, compare the value of the current node and the max values on the 2 sides of it, then return the max of those 3 values
    public int GetMaximumElementBetweenNodes(int node1, int node2)
    {
        if (_root is null) return -1;

        if(node1 > node2)
        {
            int temp = node1;
            node1 = node2;
            node2 = temp;
        }
        var lcaNode = FindLowestCommonAncestor(node1, node2);
        if (lcaNode is null) return -1;
        int max = GetMaximum(lcaNode, node1, node2);
        return max;
    }
    private Node? FindLowestCommonAncestor(int node1, int node2)
    {
        if (_root is null) return null;
        var temp = _root;
        while(temp != null)
        {
            if (node1 < temp.Data && temp.Data < node2)
                return temp;

            if(node1 == temp.Data || node2 == temp.Data)
                return temp;

            if(node1 < temp.Data && node2 < temp.Data)
                temp = temp.LeftNode;
            else if(node1 > temp.Data && node2 > temp.Data)
                temp = temp.RightNode;  
        }

        return null;
    }
    private int GetMaximum(Node? node, int? data1, int? data2)
    {
        if(node is null) return -1;

        int maximumLeft = 0, maximumRight = 0;
        if(data1 < node.Data)
            maximumLeft = GetMaximum(node.LeftNode, data1, null);
        else if(data2 < node.Data)
            maximumLeft = GetMaximum(node.LeftNode, null, data2);

        if (data1 > node.Data)
            maximumRight = GetMaximum(node.RightNode, data1, null);
        else if(data2 > node.Data)
            maximumRight = GetMaximum(node.RightNode, null, data2);

        if (maximumLeft == -1 || maximumRight == -1) return -1;

        int max = node.Data;
        if (maximumRight > max)
            max = maximumRight;
        if (maximumLeft > max)
            max = maximumLeft;
        return max;
    }
}
