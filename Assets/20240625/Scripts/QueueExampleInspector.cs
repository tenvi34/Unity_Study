using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QueueExample))]
public class QueueExampleInspector : Editor
{
    public string Message;
    
    public override void OnInspectorGUI()
    {
        // 플레이 상태에서만 작동
        if (EditorApplication.isPlaying)
        {
            QueueExample manager = (QueueExample)target;

            Message = EditorGUILayout.TextField(Message);
            
            if (GUILayout.Button("Send Message"))
            {
                manager.RequestMessage(Message);
                Message = string.Empty;
                
                EditorUtility.SetDirty(this);
            }
        }
    }
}
