using System.Collections;
using System.Collections.Generic;

public class MyStack<T>
{
    private List<T> elements = new List<T>();
    private int size = 0;

    public void Push(T element)
    {
        elements.Add(element);
        size++;
    }

    public T Pop()
    {
        T result = elements[^1];
        elements.Remove(result);
        size--;

        return result;
    }

    public T Peek()
    {
        return elements[^1];
    }

    public int Count()
    {
        return size;
    }

    IEnumerable<T> GetElements()
    {
        return elements;
    }

}
