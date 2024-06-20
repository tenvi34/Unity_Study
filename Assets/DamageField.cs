using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageFieldEnum
{
    None,
    DamageFieldPlayerAttack1,
    DamageFieldPlayerAttack2,
    DamageFieldPlayerAttack3,
    DamageFieldPlayerAttack4,
}

public enum DamageFieldHitType
{
    None,
    Once,
    Duration,
}

public class DamageField : MonoBehaviour
{
    [SerializeField] 
    private DamageFieldDataGroup damageFieldDataGroup;
    
    // 데미지 필드 시전자
    [NonSerialized] 
    public GameObject Owner;

    // 몇명 맞는지
    private float hitCount = 0;

    [NonSerialized] 
    public DamageFieldEnum DamageFieldEnumValue;

    private DamageFieldData cacheDamageFieldData;
    
    void Start()
    {
        cacheDamageFieldData = damageFieldDataGroup.GetDamageFieldData(DamageFieldEnumValue);
        
        var damageFieldHitType = cacheDamageFieldData.damageFieldHitType;
        if (damageFieldHitType == DamageFieldHitType.Once)
        {
            StartCoroutine(OnceDamageFieldLifeTime());
        }
        else if (damageFieldHitType == DamageFieldHitType.Duration)
        {
            StartCoroutine(DurationDamageFieldLifeTime());
        }
    }

    IEnumerator OnceDamageFieldLifeTime()
    {
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    IEnumerator DurationDamageFieldLifeTime()
    {
        yield return new WaitForSeconds(cacheDamageFieldData.damageFieldDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitCount >= cacheDamageFieldData.hitMaxCount)
            return;

        hitCount++;

        DamageFieldData resultData = GetDamageFieldData(DamageFieldEnumValue);
        DamageFieldRigidbodyEffect(other, resultData.GetPushDirection(Owner, other.gameObject), resultData.power);
    }

    private DamageFieldData GetDamageFieldData(DamageFieldEnum damageFieldEnum)
    {
        return damageFieldDataGroup.GetDamageFieldData(damageFieldEnum);
    }

    private void DamageFieldRigidbodyEffect(Collider other, Vector3 direction, float power)
    {
        // 내가 내 자신을 피격하는것은 잘못된 구조이므로 Owner일 경우 무시
        if (other.gameObject == Owner)
            return;
        
        Rigidbody hitRigidbody = other.attachedRigidbody;
        if (hitRigidbody)
        {
            hitRigidbody.AddForce(direction * power, ForceMode.Impulse);
        }
    }
}
