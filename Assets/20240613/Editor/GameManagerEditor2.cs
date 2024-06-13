using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 어떤 컴포넌트를 수정할 지 정하는 것
[CustomEditor(typeof(GameManager))]

// Editor 상속
public class GameManagerEditor2 : Editor
{
    private string AnimationName;
    private float SliderValue;
    
    // 인스펙터에 어떻게 나타낼지
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimationName = EditorGUILayout.TextField("Animation Name", AnimationName);
        SliderValue = EditorGUILayout.Slider(SliderValue, 0, 1);

        GameManager manager = (GameManager)target;
        Animator animator = manager.GetComponent<Animator>();
        animator.Play(AnimationName, 0, SliderValue);
        animator.Update(Time.deltaTime);
    }
}
