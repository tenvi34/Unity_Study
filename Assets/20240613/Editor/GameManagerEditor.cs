using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManagerEditor : EditorWindow
{
    private int JumpPower;
    
    [MenuItem("JWT_Menu/GameManagerEditor")]
    public static void Show()
    {
        Debug.Log("Custom Editor");
        GetWindow<GameManagerEditor>("GameManagerEditor");
    }

    private void OnGUI()
    {
        JumpPower = EditorGUILayout.IntField("JumpPower", JumpPower);
        
        if (GUILayout.Button("Function1"))
        {
            // 캐릭터 찾기
            GameObject myCharacter = GameObject.Find("MyCharacter");
            myCharacter.GetComponent<MyCharacterController>().jumpPower = JumpPower;
        }

        if (GUILayout.Button("Internal Test"))
        {
            GameObject myCharacter = GameObject.Find("MyCharacter");
            myCharacter.GetComponent<GameManager>();
            //myCharacter.GetComponent<GameManagerInternal>();
        }
    }
}
