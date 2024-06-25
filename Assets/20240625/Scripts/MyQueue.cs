using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQueue<T>
{
    private List<T> elements;

    public void Enqueue(T element)
    {
        elements.Add(element);
    }
    
    public T Dequeue()
    {
        T result = elements[0];
        elements.RemoveAt(0);
        return result;
    }

    public T Peek()
    {
        return elements[0];
    }

    public int Count()
    {
        return elements.Count;
    }

    public IEnumerable<T> GetElements()
    {
        return elements;
    }
}
