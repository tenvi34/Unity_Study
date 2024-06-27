using UnityEngine;

public class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BinarySearchTree
{
    public Node Root;

    public BinarySearchTree()
    {
        Root = null;
    }

    // 삽입
    public void Insert(int value)
    {
        if (Root == null) 
            Root = new Node(value);
        else
            InsertRecursively(Root, value);
    }

    public Node InsertRecursively(Node node, int value)
    {
        if (value < node.Value)
        {
            node.Left = InsertRecursively(node, value);
        }
        else if (value > node.Value)
        {
            node.Right = InsertRecursively(node, value);
        }
        
        return node;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree();
        
        // 노드 삽입
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        bst.Insert(60);
        bst.Insert(80);
        
        // 노드 탐색
        //Debug.Log("Search 40: " + bst.Search(40));
        //Debug.Log("Search 25: " + bst.Search(25));
        
        // 중위 순회
        //Debug.Log("InOrder Traversal: ");
        //bst.InOrderTraversal(); // 20 30 40 50 60 70 80
    }
}
