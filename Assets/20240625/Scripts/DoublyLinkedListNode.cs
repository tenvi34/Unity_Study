using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class DoublyLinkedListNode<T>
{
    private Node<T> head; // 첫번째 노드
    private Node<T> tail; // 마지막 노드

    public DoublyLinkedListNode() // 비어있는 상태로 생성
    {
        head = null;
        tail = null;
    }
    
    // 리스트 앞에 데이터(노드) 추가
    public void AddFirst(T data)
    {
        // 추가할 data
        Node<T> newNode = new Node<T>(data);
        // 비어있을 경우 새로운 노드를 가리키도록 설정
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            // 새로운 데이터(노드)를 리스트 앞에 추가
            newNode.Next = head; // 새로운 데이터의 다음이 기존 head(맨 앞 데이터)를 가리키도록 설정
            head.Prev = newNode; // 기존 head의 앞에 새로운 데이터를 추가
            head = newNode; // head를 새로운 데이터로 재설정
        }
    }

    // 리스트 끝에 데이터(노드) 추가
    public void AddLast(T data)
    {
        // 추가할 data
        Node<T> newNode = new Node<T>(data);
        // 비어있을 경우 새로운 노드를 가리키도록 설정
        if (tail == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode; // 기존 tail(마지막 데이터)의 다음에 새로운 데이터 추가
            newNode.Prev = tail; // 새로운 데이터의 앞에 기존 tail을 이동
            tail = newNode; // 새로운 tail을 추가한 데이터로 재설정
        }
    }

    // 리스트 앞의 데이터(노드) 삭제
    public void RemoveFirst()
    {
        if (head == null) return;
        // 리스트에 데이터(노드)가 하나만 있으면
        if (head.Next == null)
        {
            head = null;
            tail = null;
        }
        else
        {
            head = head.Next; // 기존 head(맨 앞 데이터)를 다음 데이터(노드)로 재설정
            head.Prev = null; // 기존 head가 다음 노드로 넘어갔으니 맨앞 데이터는 head.Prev이며 null로 하면서 삭제
        }
    }
    
    // 리스트 끝의 데이터(노드) 삭제
    public void RemoveLast()
    {
        if (tail == null) return;
        // 리스트에 데이터(노드)가 하나만 있으면 (head == tail)
        if (tail.Prev == null)
        {
            head = null;
            tail = null;
        }
        else
        {
            tail = tail.Prev; // 기존 tail(맨 뒤 데이터)을 tail 앞의 데이터(노드)로 재설정
            tail.Next = null; // 기존 tail이 앞의 노드로 이동했으니 맨 뒤의 데이터는 tail.Next이며 null로 하면서 삭제
        }
    }

    // 특정 데이터 삭제
    public void RemoveIndex(T data)
    {
        if (head == null && tail == null) return;

        // 맨 앞부터 검사 시작
        Node<T> current = head;

        while (current != null)
        {
            // 삭제할 데이터와 일치하는 데이터를 찾았을 때
            if (current.Data.Equals(data))
            {                
                if (current.Prev == null) RemoveFirst(); // 현재 데이터가 head일 경우
                else if (current.Next == null) RemoveLast(); // 현재 데이터가 tail일 경우
                else if (current.Prev != null && current.Next != null) // 중간 데이터일 경우
                {
                    current.Prev.Next = current.Next; // 삭제할 데이터 기준 이전의 데이터가 삭제할 데이터를 넘어서 다음 데이터로 연결
                    current.Next.Prev = current.Prev; // 삭제할 데이터 기준 다음의 데이터가 삭제할 데이터의 앞에 데이터로 연결
                }
                return;
            }
            current = current.Next;
        }
    }

    public void PrintFirst()
    {
        Node<T> current = head;
        while (current != null)
        {
            Debug.Log("Data: " + current);
            current = current.Next;
        }
    }

    public void PrintLast()
    {
        Node<T> current = tail;
        while (current != null)
        {
            Debug.Log("Data: " + current);
            current = current.Prev;
        }
    }
}
