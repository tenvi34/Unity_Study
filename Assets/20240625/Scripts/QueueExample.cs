using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample : MonoBehaviour
{
    private Queue<string> messageQueue = new Queue<string>();
    //public string Message;

    void Start()
    {
        StartCoroutine(MessageQueueProcess());
    }

    // private void OnGUI()
    // {
    //     if (GUI.Button(new Rect(10, 10, 150, 100), "Send Message"))
    //     {
    //         messageQueue.Enqueue(Message);
    //         Message = string.Empty;
    //         
    //         EditorUtility.SetDirty(this);
    //     }
    // }

    public void RequestMessage(string msg)
    {
        messageQueue.Enqueue(msg);
    }

    IEnumerator MessageQueueProcess()
    {
        while (true)
        {
            string msg;
            if (messageQueue.TryDequeue(out msg))
            {
                Debug.Log(msg);
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }
}
