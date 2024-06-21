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
    DamageFieldPlayerAttack4
}

public enum DamageFieldHitType
{
    None,
    Once,
    Duration
}

public class DamageField : MonoBehaviour
{
    [SerializeField] private DamageFieldDataGroup _damageFieldDataGroup;
    
    // 데미지 필드 주인
    [NonSerialized]
    public GameObject Owner;
    
    [NonSerialized] 
    public DamageFieldEnum DamageFieldEnum_V;
    
    // 몇명 맞았는지
    private float _hitCount = 0;

    private DamageFieldData _cachedDamageFieldData;
    
    // Start is called before the first frame update
    void Start()
    {
        _cachedDamageFieldData = _damageFieldDataGroup.GetDamageFieldData(DamageFieldEnum_V);
        var damageFieldHitType = _cachedDamageFieldData.DamageFieldHitType;
        if (damageFieldHitType == DamageFieldHitType.Once)
        {
            StartCoroutine(OnceDamageFieldLifeTime());
        }
        else if (damageFieldHitType == DamageFieldHitType.Duration)
        {
            StartCoroutine(DurationDamageFieldLifeTime());
        }
    }

    IEnumerator DurationDamageFieldLifeTime()
    {
        yield return new WaitForSeconds(_cachedDamageFieldData.DamageFieldDuration);
        Destroy(gameObject);
    }

    IEnumerator OnceDamageFieldLifeTime()
    {
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // hitMaxCount보다 맞은게 더 많으면 더 이상 충돌은 하지 않는다.
        if (_hitCount >= _cachedDamageFieldData.HitMaxCount)
            return;

        _hitCount++;
        
        DamageFieldData resultData = GetDamageFieldData(DamageFieldEnum_V);
        DamageFieldRigidbodyEffect(other, resultData.GetPushDirection(Owner, other.gameObject), resultData.power);
    }

    private DamageFieldData GetDamageFieldData(DamageFieldEnum damageFieldEnum)
    {
        return _damageFieldDataGroup.GetDamageFieldData(damageFieldEnum);
    }

    private void DamageFieldRigidbodyEffect(Collider other, Vector3 direction, float power)
    {
        // 내가 내 자신을 때리는건 이상하기 때문에 Owner이면 무시한다
        if (other.gameObject == Owner)
            return;

        Rigidbody hitRigidbody = other.attachedRigidbody;
        if (hitRigidbody)
        {
            hitRigidbody.AddForce(direction * power, ForceMode.Impulse);
        }
    }
}
