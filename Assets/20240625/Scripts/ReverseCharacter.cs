using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReverseCharacter : MonoBehaviour
{
    public string data;
    private Stack<char> charStacks = new Stack<char>();
    
    void Start()
    {
        Debug.Log("Origin: " + data);
        Debug.Log( "Reverse: " + ToReverse());
    }

    string ToReverse()
    {
        // Reverse() 직접 구현해보기
        //data.Reverse();
        
        foreach (var c in data)
        {
            charStacks.Push(c);
        }

        string result = string.Empty;

        while (charStacks.Count > 0)
        {
            result += charStacks.Pop();
        }

        return result;
    }

    void Update()
    {
        
    }
}
