using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArrayQueue<T>
{   
    private T[] elements;
    private T emptyValue;
    
    private int head;
    private int tail;
    private int size;
    private int capacity;

    public void SetCapacity(int _capa)
    {
        T[] oldElement = elements;
        
        capacity = _capa;
        elements = new T[_capa];

        for (int i = 0; i < oldElement.Length; i++)
        {
            if (i >= capacity) break;
            elements[i] = oldElement[i];
        }
        
        head = 0;
        tail = 0;
        size = 0;
    }

    public void Enqueue(T element)
    {
        if (elements.Length >= size)
        {
            Debug.Log("Out of Range");
            return;
        }

        elements[tail] = element;
        tail += 1;
        size++;
    }

    public T Dequeue()
    {
        if (size <= 0)
        {
            Debug.Log("Is Empty");
            return emptyValue;
        }
        
        T result = elements[head];
        head += 1;
        size--;
        
        return result;
    }

    public T Peek()
    {
        return elements[head];
    }
    
}
