using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimNotification : MonoBehaviour
{
    private NewChanController _newChanController;
    
    private void Awake()
    {
        _newChanController = GetComponent<NewChanController>();
        if (!_newChanController) Debug.Log("NewChanController not found");
    }

    public void OnNotify(string sValue)
    {
        switch (sValue)
        {
            case "OnHit":
                _newChanController?.OnHit();
                break;
            default:
                Debug.Log($"OnNotify {sValue} not defined");
                break;
        }
    }
}
