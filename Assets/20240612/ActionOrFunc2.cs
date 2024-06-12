using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrFunc2 : MonoBehaviour
{
    // 반환값 X
    private Action<int, float, bool> _action;
    public void RegistAction(Action<int, float, bool> __action)
    {
        _action = __action;
    }
    
    // 마지막 parameter가 반환값
    private Func<int, float, string> _func;
    public void RegistFunc(Func<int, float, string> __func)
    {
        _func = __func;
    }
    
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
        // _action = null;
        //
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     _action = Func1;
        // }
        // else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     _action = Func2;
        // }
        // else if (Input.GetKeyDown(KeyCode.D))
        // {
        //     _action = Func3;
        // }
        // else if (Input.GetKeyDown(KeyCode.F))
        // {
        //     _action = Func4;
        // }
        // else if (Input.GetKeyDown(KeyCode.G))
        // {
        //     _action = Func5;
        // }
    }

    private void LateUpdate()
    {
        _action?.Invoke(1, 2.0f, true);
        string printResult = _func?.Invoke(1, 1.0f);
        if (printResult != null) Debug.Log(printResult);
    }
}
