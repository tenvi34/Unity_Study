using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageFieldEnum
{
    None,
    DamageFieldPlayerPunch1,
    DamageFieldPlayerPunchL,
    DamageFieldPlayerKickR,
    DamageFieldPlayerKickL,
}

public enum DamageFieldHitType
{
    None,
    Once,
    Duration,
}

public class DamageField : MonoBehaviour
{
    // 데미지 필드 이름
    [SerializeField] 
    private DamageFieldEnum damageFieldEnum;
    // 히트 타입
    [SerializeField] 
    private DamageFieldHitType damageFieldHitType;
    // 데미지 필드 지속시간
    [SerializeField] 
    private float damageFieldDuration;
    // 데미지 필드 최대 히트 카운트
    [SerializeField] 
    private float hitMaxCount;
    // 데미지 필드 시전자
    [NonSerialized] 
    public GameObject Owner;

    private float hitCount = 0;
    
    void Start()
    {
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
        yield return new WaitForSeconds(damageFieldDuration);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitCount >= hitMaxCount)
            return;

        hitCount++;
        
        switch (damageFieldEnum)
        {
            case DamageFieldEnum.DamageFieldPlayerPunch1:
                // 내가 내 자신을 피격하는것은 잘못된 구조이므로 Owner일 경우 무시
                if (other.gameObject == Owner)
                    return;
                
                Rigidbody hitRigidbody = other.attachedRigidbody;
                if (hitRigidbody)
                {
                    Vector3 direction = other.transform.position - Owner.transform.position;
                    direction.Normalize();
                    
                    hitRigidbody.AddForce(Owner.transform.forward * 6.0f, ForceMode.Impulse);
                }
                break;
            case DamageFieldEnum.DamageFieldPlayerPunchL:
                if (other.gameObject == Owner)
                    return;
                
                break;
            case DamageFieldEnum.DamageFieldPlayerKickR:
                if (other.gameObject == Owner)
                    return;
                
                break;
            case DamageFieldEnum.DamageFieldPlayerKickL:
                if (other.gameObject == Owner)
                    return;
                
                break;
        }
    }
}
