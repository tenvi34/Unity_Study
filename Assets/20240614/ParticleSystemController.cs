using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public void OnHit(GameObject template,Transform parent, Vector3 spawnPosition, Vector3 spawnDirection)
    {
        // Prefab을 GameObject로 만들어주는 C# new 키워드처럼 작동
        // spawnPosition과 spawnDirection을 넣어서 생성 위치를 결정
        GameObject newObject = 
            Instantiate(template, spawnPosition, Quaternion.LookRotation(spawnDirection));
        
        // 부모 인자값이 null이 아니면 만들어준다
        if(parent != null) newObject.transform.SetParent(parent);
    }
}
