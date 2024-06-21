using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DamageFieldCollisionData))]
public class DamageFieldCollisionDataDrawerUIE : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 1;
        
        SerializedProperty damageFieldCollisionType = property.FindPropertyRelative("damageFieldCollisionType");
        DamageFieldCollisionType damageFieldCollisionTypeEnum = (DamageFieldCollisionType)damageFieldCollisionType.intValue;
        
        if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Sphere)
        {
            lineCount++;
        }
        else if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Box)
        {
            lineCount++;
        }
        else if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Capsule)
        {
            lineCount += 2;
        }
        
        return base.GetPropertyHeight(property, label) * lineCount;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // UI 오버라이드 시작
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty damageFieldCollisionType = property.FindPropertyRelative("damageFieldCollisionType");
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, damageFieldCollisionType);
        
        // 1칸 들여쓰기
        EditorGUI.indentLevel++;
        
        // DamageFieldCollisionData의 enum을 intValue에 저장
        DamageFieldCollisionType damageFieldCollisionTypeEnum = (DamageFieldCollisionType)damageFieldCollisionType.intValue;
        if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Sphere)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("SphereRadius"));
        }
        else if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Box)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("BoxSize"));
        }
        else if (damageFieldCollisionTypeEnum == DamageFieldCollisionType.Capsule)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("CapsuleRadius"));
            
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("CapsuleHeight"));
        }
        
        // 1칸 내어쓰기
        EditorGUI.indentLevel--;
        
        // UI 오버라이드 종료
        EditorGUI.EndProperty();
        
    }
}
