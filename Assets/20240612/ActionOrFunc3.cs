using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrFunc3 : MonoBehaviour
{
    public ActionOrFunc2 _actionOrFunc2;
    
    void Func1(int a, float b, bool c)
    {
        Debug.Log("Func1");
    }
    void Func2(int a, float b, bool c)
    {
        Debug.Log("Func2");
    }
    void Func3(int a, float b, bool c)
    {
        Debug.Log("Func3");
    }
    void Func4(int a, float b, bool c)
    {
        Debug.Log("Func4");
    }
    void Func5(int a, float b, bool c)
    {
        Debug.Log("Func5");
    }
    
    string Func_Call1(int a, float bc)
    {
        return "Func1";
    }
    string Func_Call2(int a, float bc)
    {
        return "Func2";
    }
    string Func_Call3(int a, float bc)
    {
        return "Func3";
    }
    string Func_Call4(int a, float bc)
    {
        return "Func4";
    }
    string Func_Call5(int a, float bc)
    {
        return "Func5";
    }
    
    void Start()
    {
        _actionOrFunc2 = GetComponent<ActionOrFunc2>();
    }

    void Update()
    {
        // Func 일 때 아래 함수 사용
        _actionOrFunc2.RegistFunc(null);
        if (Input.GetKeyDown(KeyCode.A))
        {
            _actionOrFunc2.RegistFunc(Func_Call1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _actionOrFunc2.RegistFunc(Func_Call2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _actionOrFunc2.RegistFunc(Func_Call3);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _actionOrFunc2.RegistFunc(Func_Call4);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _actionOrFunc2.RegistFunc(Func_Call5);
        }
        
        // // Action 일 때 아래 함수 사용
        // _actionOrFunc2.RegistAction(null);
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     _actionOrFunc2.RegistAction(Func1);
        // }
        // else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     _actionOrFunc2.RegistAction(Func2);
        // }
        // else if (Input.GetKeyDown(KeyCode.D))
        // {
        //     _actionOrFunc2.RegistAction(Func3);
        // }
        // else if (Input.GetKeyDown(KeyCode.F))
        // {
        //     _actionOrFunc2.RegistAction(Func4);
        // }
        // else if (Input.GetKeyDown(KeyCode.G))
        // {
        //     _actionOrFunc2.RegistAction(Func5);
        // }
    }
}
