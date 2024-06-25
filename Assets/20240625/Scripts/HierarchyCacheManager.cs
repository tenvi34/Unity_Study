using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HierarchyCacheManager : MonoBehaviour
{
    public string CachedFootPath = string.Empty;
    private GameObject CachedFootGO = null;
    
    void Awake()
    {
        string[] splited = CachedFootPath.Split('.');
        Transform Found = FindRecursiveObject(transform, splited.ToList(), 0);
        Debug.Log(Found);
    }

    Transform FindRecursiveObject(Transform tr, List<string> recursiveNames, int currentIdx)
    {
        if (currentIdx >= recursiveNames.Count - 1)
        {
            return tr.Find(recursiveNames[currentIdx]);
        }
        
        Debug.Log(recursiveNames[currentIdx]);
        
        Transform nextFinder = tr.Find(recursiveNames[currentIdx]);
        if (nextFinder.name == recursiveNames[currentIdx])
        {
            return FindRecursiveObject(nextFinder, recursiveNames, ++currentIdx);
        }

        return null;
    }
}
