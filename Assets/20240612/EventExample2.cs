using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExample2 : MonoBehaviour
{
    public EventExample eventObject;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            eventObject.originalDelegate += AttachFunc;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            eventObject.originalEvent += AttachFunc;
        }
    }

    void AttachFunc()
    {
        
    }
}
