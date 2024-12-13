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
}
