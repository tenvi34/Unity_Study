using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoActionOrFunc : MonoBehaviour
{
    string funcName;
    // normalcase
    void Func1()
    {
        Debug.Log("Func1");
    }
    void Func2()
    {
        Debug.Log("Func2");
    }
    void Func3()
    {
        Debug.Log("Func3");
    }
    void Func4()
    {
        Debug.Log("Func4");
    }
    void Func5()
    {
        Debug.Log("Func5");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        funcName = "";
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            funcName = "Func1";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            funcName = "Func2";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            funcName = "Func3";
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            funcName = "Func4";
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            funcName = "Func5";
        }
        
    }

    void LateUpdate()
    {
        if (funcName == "Func1")
        {
            Func1();
        }
        else if (funcName == "Func2")
        {
            Func2();
        }
        else if (funcName == "Func3")
        {
            Func3();
        }
        else if (funcName == "Func4")
        {
            Func4();
        }
        else if (funcName == "Func5")
        {
            Func5();
        }
    }
}
