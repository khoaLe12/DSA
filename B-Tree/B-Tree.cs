using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree;

internal class B_Tree
{
    private Node? root = null;
    public int Order { get; private set; }

    public B_Tree(int order)
    {
        Order = order;
    }

    public void Traverse()
    {
        if (root == null) return;
        root.Traverse();
    }

    // In the current node does not have te key, find on its child node
    // (The child which is just before the first greater key)
    // The index of child is the index of first greater key
    public Node? Search(int data)
    {
        if (root is null) return null;
        return root.Search(data);
    }


    // Recursively finding the position to insert key from root to leaf
    // The insert operation required the handling on the overflow of node (on number of keys and number of children)
    // If the node overflows (has more keys than order-1), split the node into 2 other nodes, promote the key to the parent node (parent node of it will have 1 more key and 1 more child)
    // If the node overflows (has more children than order), split that node into 2 other nodes, each new node has an appropriate number of children (parent node of it will have 1 more child)
    // Recursively checking the condition of node from leaf to root
    // If the overflows happens at root, the height of tree will be increased by one, new root will be assigned with the value of middle key of old root, old root is divided by 2 other nodes
    public void Insert(int data)
    {
        if (root is null)
        {
            var node = new Node(Order);
            node.InsertKey(data);
            root = node;
            return;
        }

        var temp = root;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(temp);
        while (temp != null && !temp.IsLeaf)
        {
            int i = 0;
            while (i < temp.Count)
            {
                if (data == temp.Keys[i]) return;
                if (data > temp.Keys[i]) i++;
                else break;
            }
            temp = temp.Child[i];
            stack.Push(temp!);
        }


    }
    private Node[] InsertKey(Node root, int key, out int promoteKey)
    {
        promoteKey = 0;

        if (root.IsLeaf)
        {
            if(root.Count == root.Keys.Length)
            {
                // Keys is full
                // Split the node and return array of new node
                
                // Create a copy of keys that contains new key
                int temp = key, index = 0;
                int[] newKeys = new int[Order];
                for(;index < root.Count; index++)
                {
                    if(temp < root.Keys[index])
                    {
                        newKeys[index] = temp;
                        temp = root.Keys[index];
                    }
                    else newKeys[index] = root.Keys[index];
                }
                newKeys[index] = temp;

                var middleIndex = Order / 2;
                promoteKey = newKeys[middleIndex];

                // Split here
                var leftChild = new Node(Order);
                var rightChild = new Node(Order);
                index = 0;
                for(; index < middleIndex; index++)
                    leftChild.InsertKey(newKeys[index]);
                for (index++; index < Order; index++)
                    rightChild.InsertKey(newKeys[index]);

                return new Node[2] { leftChild, rightChild };
            }
            root.InsertKey(key);
            return new Node[] { root };
        }

        // Find the index of child node to move next
        int i = 0;
        while (i < root.Count)
        {
            if (key == root.Keys[i]) return new Node[] {root};
            if (key > root.Keys[i]) i++;
            else break;
        }

        // Overflows happened at root.Child[i] if the result count = 2 / child node is split into 2 other nodes
        var result = InsertKey(root.Child[i]!, key, out int promotedKey);
        if (result.Count() == 2)
        {
            if (root.Count == root.Keys.Length)
            {
                // Split the parent node, get the new list of keys and child nodes
                int temp = promotedKey, index = 0, count = 0;
                int[] newKeys = new int[Order];
                Node[] childNodes = new Node[Order + 1];

                for (; index < root.Count; index++)
                {
                    if (temp < root.Keys[index])
                    {
                        newKeys[index] = temp;
                        temp = root.Keys[index];
                    }
                    else newKeys[index] = root.Keys[index];
                }
                newKeys[index] = temp;
                index = 0;
                for (; index < Order; index++, count++)
                {
                    if (index == i)
                    {
                        childNodes[count++] = result[0];
                        childNodes[count++] = result[1];
                        index++;
                    }
                    childNodes[count] = root.Child[index]!;
                }

                var middleIndex = Order / 2;
                promoteKey = newKeys[middleIndex];


                // Split here
                var leftChild = new Node(Order);
                var rightChild = new Node(Order);
                index = 0; count = 0;
                for (; index < middleIndex; index++, count++)
                {
                    leftChild.InsertKey(newKeys[index]);
                    leftChild.Child[count] = childNodes[index];
                }
                leftChild.Child[count] = childNodes[index];
                count = 0;

                for (index++; index < Order; index++)
                {
                    rightChild.InsertKey(newKeys[index]);
                    leftChild.Child[count] = childNodes[index];
                }

                return new Node[2] { leftChild, rightChild };
            }

            if (root.InsertKey(promotedKey))
            {
                root.Child[i] = result[0];
                var temp = root.Child[++i];
                root.Child[i] = result[1];
                for (i++; i < root.Count; i++)
                {
                    var tempChild = root.Child[i];
                    root.Child[i] = temp;
                    temp = tempChild;
                }
            }
        }

        return new Node[] { root };
    }


    // Traverse from root to leaf
    // If leaf is full, split the leaf -> if parent is full, split the parent -> if parent is full, split the parent -> ...
    // Using recursive to split from the most parent that is full to the leaf
    // Then insert key to the split one
    private void InsertKey(Node parent, Node? root, ref int? key)
    {
        if (root is null) return;

        if (root.IsLeaf)
        {
            if (root.Count == Order - 1)
                key = parent.SplitChildWithKey(root, key);
            else
            {
                root.InsertKey(key);
                key = null;
            }
            return;
        }

        int i = 0;
        while (i < root.Count)
        {
            if (key == root.Keys[i]) return;
            if (key > root.Keys[i]) i++;
            else
            {
                InsertKey(root, root.Child[i], ref key);
                if (key is null) return;
                key = parent.SplitChildWithKey(root, key);
            }
        }
    }
}
