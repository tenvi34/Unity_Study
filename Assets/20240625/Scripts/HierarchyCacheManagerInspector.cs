using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HierarchyCacheManager))]
public class HierarchyCacheManagerInspector : Editor
{
    public Object FoundObject;
    
    public override void OnInspectorGUI()
    {
        HierarchyCacheManager manager = (HierarchyCacheManager)target;

        EditorGUI.BeginChangeCheck();
        FoundObject = EditorGUILayout.ObjectField(FoundObject, typeof(GameObject), true);

        if (EditorGUI.EndChangeCheck())
        {
            if (FoundObject != null)
            {
                manager.CachedFootPath = GetFullPath(((GameObject)FoundObject).transform);                
            }
            else
            {
                manager.CachedFootPath = string.Empty;
            }
            EditorUtility.SetDirty(manager);
        }
        
        EditorGUILayout.LabelField(manager.CachedFootPath);
    }

    private string GetFullPath(Transform tr)
    {
        var parents = tr.GetComponentsInParent<Transform>();

        var str = new StringBuilder(parents[^2].name);
        for (var i = parents.Length - 2; i >= 0; i--)
            str.Append($".{parents[i].name}");
        
        return str.ToString();
    }
}
