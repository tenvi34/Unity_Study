using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotifyState
{
    None,
    OnHitLeftPunch,
    OnHitRightPunch,
    OnHitLeftKick,
    OnHitRightKick,
}

public class AnimNotification : MonoBehaviour
{
    private NewChanController _newChanController;
    
    private void Awake()
    {
        _newChanController = GetComponent<NewChanController>();
        if (!_newChanController) Debug.Log("NewChanController not found");
    }

    public void OnNotify(NotifyState state)
    {
        switch (state)
        {
            case NotifyState.OnHitLeftPunch:
                _newChanController?.OnHit_PunchLeft();
                break;
            case NotifyState.OnHitRightPunch:
                _newChanController?.OnHit_PunchRight();
                break;
            case NotifyState.OnHitLeftKick:
                _newChanController?.OnHit_KickLeft();
                break;
            case NotifyState.OnHitRightKick:
                _newChanController?.OnHit_KickRight();
                break;
            default:
                Debug.Log($"OnNotify {state} not defined");
                break;
        }
    }
}
