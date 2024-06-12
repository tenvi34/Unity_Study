using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventExampleDelegate();

public class EventExample : MonoBehaviour
{
    public EventExampleDelegate originalDelegate;
    public event EventExampleDelegate originalEvent;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
