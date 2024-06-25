using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArrayStack<T>
{
    private T[] elements;
    private int size;
    private int capacity;

    public MyArrayStack()
    {
        SetCapacity(4);
    }

    private void SetCapacity(int capacity_)
    {
        // capacity_가 0보다 작거나 같은 경우 예외처리
        Debug.Assert(!(capacity_ <= 0), "Invalid capacity");

        if (elements == default)
        {
            elements = new T[capacity_];
            size = 0;
        }
        else
        {
            // capacity_를 늘리는 경우 value가 존재하는 size 까지만 순회
            // capacity_를 줄이는 경우 뒷자리에 남는 값은 메모리에 반환
            int length_ = -1;
            if (size < capacity_) { length_ = size; }
            else { length_ = capacity_; }

            T[] newElements = new T[capacity_];
            for (int i = 0; i < length_; i++)
            {
                newElements[i] = elements[i];
                size = i + 1;
            }
            elements = newElements;
        }
        capacity = capacity_;
    }   // SetCapacity()

    public void Push(T element_)
    {
        elements[size] = element_;
        size++;

        if(capacity <= size + 1)
        {
            SetCapacity(capacity * 2);
        }   // if: capacity가 부족하면 리사이즈
    }   // Push()

    public T Pop()
    {
        // size가 0이하인 경우 예외처리
        if (size <= 0) { return default(T); }

        T result = elements[size-1];
        elements[size-1] = default;
        size--;

        if(size * 4 < capacity)
        {
            SetCapacity(capacity / 2);
        }   // if: capacity가 너무 남으면 리사이즈

        return result;
    }   // Pop()

    public T Peek()
    {
        return elements[size-1];
    }

    public int Count()
    {
        return size;
    }

    public IEnumerable<T> GetElements()
    {
        return elements;
    }
}
