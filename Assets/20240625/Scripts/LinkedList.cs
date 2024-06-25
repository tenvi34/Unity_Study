using System.Collections;
using System.Collections.Generic;

public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }
    public Node<T> Prev { get; set; }

    public Node(T _data)
    {
        Data = _data;
        Next = null;
        Prev = null;
    }
}

public class LinkedList<T>
{
    private Node<T> head; // 첫번째 노드
    private Node<T> tail; // 마지막 노드

    public LinkedList() // 비어있는 상태로 생성
    {
        head = null;
        tail = null;
    }
    
    // 리스트 앞에 데이터(노드) 추가
    public void AddFirst(T data)
    {
        // 추가할 data
        Node<T> newNode = new Node<T>(data);
        // 비어있을 경우
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
    }

    // 리스트 끝에 데이터(노드) 추가
    public void AddLast(T data)
    {
        // 추가할 data
        Node<T> newNode = new Node<T>(data);
        // 비어있을 경우
        if (tail == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }
    
    // 특정 데이터 삭제
    public void Remove(T data)
    {
        Node<T> current = head;

        // 비어있으면 종료
        if (current == null) return;

        if (current.Prev != null)
        {
            current.Prev.Next = current.Next;
        }
        else
        {
            head = current.Next;
        }

        if (current.Next != null)
        {
            current.Next.Prev = current.Prev;
        }
        else
        {
            tail = current.Prev;
        }
    }
}
