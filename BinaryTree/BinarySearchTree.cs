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

}
