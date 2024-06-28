using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyCacheManager : MonoBehaviour
{
    // 캐릭터 관절 구조 바인딩하지 않고 스크립트로 찾음.
    public string CacheFootPath = string.Empty;

    public GameObject CacheFootGO = null;

    private void Awake()
    {
        string[] splited = CacheFootPath.Split('.');
    }

    Transform FindRecursiveObject(List<string> recursiveNames, int currentIndex)
    {
        if (currentIndex >= recursiveNames.Count)
            return null;
        
        Transform nextFinder = transform.Find(recursiveNames[currentIndex]);
        
        if (nextFinder.name == recursiveNames[currentIndex])
            return nextFinder;
        
        return FindRecursiveObject(recursiveNames, ++currentIndex);
    }
}
