namespace BinaryTree;

internal class BinaryTree
{
    private Node? _root;

    public BinaryTree(Node root)
    {
        _root = root;
    }

    // DFS Traversal
    public void InorderDFS()
    {
        if (_root is null) return;
        InoderTraversal(_root);
    }
    private void InoderTraversal(Node? node)
    {
        if (node is null) return;
        InoderTraversal(node.LeftNode);
        Console.Write($"{node.Data} ");
        InoderTraversal(node.RightNode);
    }

    public void PreorderDFS()
    {
        if(_root is null) return;
        PreorderTraversal(_root);
    }
    private void PreorderTraversal(Node? node)
    {
        if (node is null) return;
        Console.Write($"{node.Data} ");
        PreorderTraversal(node.LeftNode);
        PreorderTraversal(node.RightNode);
    }

    public void PostorderDFS()
    {
        if (_root is null) return;
        PostorderTraversal(_root);
    }
    private void PostorderTraversal(Node? node)
    {
        if (node is null) return;
        PostorderTraversal(node.LeftNode);
        PostorderTraversal(node.RightNode);
        Console.Write($"{node.Data} ");
    }

    // BFS Traversal
    public void BFS()
    {
        if (_root is null) return;
        Queue<Node> queue = new();
        queue.Enqueue(_root);
        while(queue.Count() > 0)
        {
            var node = queue.Dequeue();
            Console.Write($"{node.Data} ");
            if (node.LeftNode is not null) queue.Enqueue(node.LeftNode);
            if (node.RightNode is not null) queue.Enqueue(node.RightNode);
        }
    }


    // Insert
    public void InsertAtNode(Node node, int data)
    {
        Queue<Node> queue = new();
        queue.Enqueue(node);

        while (true)
        {
            var cuurentNode = queue.Dequeue();
            if (cuurentNode.LeftNode is null)
            {
                cuurentNode.LeftNode = new Node(data);
                return;
            }
            else
            {
                queue.Enqueue(cuurentNode.LeftNode);
            }

            if (cuurentNode.RightNode is null)
            {
                cuurentNode.RightNode = new Node(data);
                return;
            }
            else
            {
                queue.Enqueue(cuurentNode.RightNode);
            }
        }
    }


    // Search
    public Node? SearchDFS(int data)
    {
        if (_root is null) return null;
        return SearchDFS(data, _root);
    }
    private Node? SearchDFS(int data, Node? node)
    {
        if(node is null) return null;

        // current
        if (node?.Data == data) return node;

        // left
        var node1 = SearchDFS(data, node?.LeftNode);
        if (node1 is not null) return node1;

        // right
        var node2 = SearchDFS(data, node?.RightNode);
        if (node2 is not null) return node2;

        return null;
    }


    public Node? SearchBFS(int data)
    {
        if (_root is null) return null;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);

        while(queue.Count > 0)
        {
            Node node = queue.Dequeue();
            if(node.Data == data) return node;

            if(node.LeftNode is not null)
                queue.Enqueue(node.LeftNode);
            if(node.RightNode is not null)
                queue.Enqueue(node.RightNode);
        }

        return null;
    }


    // Delete
    public void DeleteNode(int data)
    {
        if(_root is null) return;

        // Find the node and its parent/ancestor
        Node? node = null, parentNode = null;
        if (_root.Data == data)
            node = _root;
        else
        {
            Queue<Node?> queue = new Queue<Node?>();
            queue.Enqueue(_root);
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                if (node is null) continue;

                if (node?.LeftNode?.Data == data)
                {
                    parentNode = node;
                    node = node?.LeftNode;
                    break;
                }
                if (node?.RightNode?.Data == data)
                {
                    parentNode = node;
                    node = node?.RightNode;
                    break;
                }

                queue.Enqueue(node?.LeftNode);
                queue.Enqueue(node?.RightNode);
            }
        }
        if (node is null) return;

        if (node.RightNode is not null)
            ReplaceNode(node, node.RightNode);
        else if(node.LeftNode is not null)
            ReplaceNode(node, node.LeftNode);
        else
        {
            // This node is a leaf, or could be a root
            // If it is a root => this tree only have 1 node, just set root to null
            // Or if it is a leaf, needs to delete node at the position of its parent
            if (object.ReferenceEquals(_root, node))
                _root = null;
            else if (object.ReferenceEquals(parentNode?.RightNode, node))
                parentNode.RightNode = null;
            else if (object.ReferenceEquals(parentNode?.LeftNode, node))
                parentNode.LeftNode = null;
        }
    }
    private void ReplaceNode(Node src, Node replaceNode)
    {
        src.Data = replaceNode.Data;

        if(replaceNode.RightNode is not null)
            ReplaceNode(replaceNode, replaceNode.RightNode);
        else if(replaceNode.LeftNode is not null)
            ReplaceNode(replaceNode, replaceNode.LeftNode);
        else
        {
            // Reach to the leaf node
            if (object.ReferenceEquals(src.RightNode, replaceNode))
                src.RightNode = null;
            else if(object.ReferenceEquals(src.LeftNode, replaceNode))
                src.LeftNode = null;
        }
    }


    // Find Height of the tree using BFS method
    public int GetHeightBFS()
    {
        // Seperate each level by a null node
        if (_root is null)
            return -1;

        int height = 0;
        Queue<Node?> queue = new();

        queue.Enqueue(_root);
        queue.Enqueue(null);

        while (queue.Count > 0)
        {
            Node? node = queue.Dequeue();
            if(node is null)
            {
                // Before it reach to a null Node, it has already traversed all nodes at a specific level
                // And enqueue new descendants (next level)
                height++;
                if (queue.Count > 0)
                    queue.Enqueue(null); // Seperate next level before traverse to the next
            }
            else
            {
                if (node.LeftNode is not null) queue.Enqueue(node.LeftNode);
                if (node.RightNode is not null) queue.Enqueue(node.RightNode);
            }
        }

        return height - 1;
    }
    // Find Height of the tree using DFS method
    public int GetHeightDFS()
    {
        return GetHeight(_root);
    }
    private int GetHeight(Node? node)
    {
        if (node == null)
            return -1;

        int lHeight = GetHeight(node?.LeftNode);
        int rHeight = GetHeight(node?.RightNode);

        int height = lHeight;
        if(rHeight > lHeight)
            height = rHeight;
        return height + 1;
    }


    // Find ancestor of a node
    public Node? FindAncestor(Node node)
    {
        if (_root is null || object.ReferenceEquals(node, _root))
            return null;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);
        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            if(object.ReferenceEquals(cur.LeftNode, node) ||
                object.ReferenceEquals(cur.RightNode, node))
                return cur;

            if (cur.LeftNode is not null) queue.Enqueue(cur.LeftNode);
            if (cur.RightNode is not null) queue.Enqueue(cur.RightNode);
        }

        return null;
    }


    // Find descendants of a node
    public IEnumerable<Node> FindDescendants(Node node)
    {
        if (_root is null)
            return Enumerable.Empty<Node>();

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);
        while(queue.Count > 0)
        {
            var cur = queue.Dequeue();
            if(object.ReferenceEquals(cur, node))
            {
                return new List<Node?>() { cur.LeftNode, cur.RightNode }.Where(n => n != null)!;
            }

            if (cur.LeftNode is not null) queue.Enqueue(cur.LeftNode);
            if (cur.RightNode is not null) queue.Enqueue(cur.RightNode);
        }
        return Enumerable.Empty<Node>();
    }
}
