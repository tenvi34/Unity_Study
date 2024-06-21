using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum DamageFieldKnockbackType
{
    None,
    Forward,
    Up
}

public enum DamageFieldCollisionType
{
    None,
    Sphere,
    Box,
    Capsule
}

[Serializable]
public class DamageFieldCollisionData
{
    
    // 어떤 컬리전을 사용할지
    public DamageFieldCollisionType damageFieldCollisionType;
    
    // 스피어일 경우 범위
    public float SphereRadius;
    
    // 박스일 경우 크기
    public Vector3 BoxSize;

    // 캡슐일 경우 높이와 범위
    public float CapsuleHeight;
    public float CapsuleRadius;
    
}



[Serializable]
public class DamageFieldData
{
    public DamageFieldCollisionData damageFieldCollisionData;
    
    [SerializeField] 
    private DamageFieldKnockbackType _damageFieldKnockbackType;
    
    // 미는 힘
    public float power;
    
    // 어떤 히트 타입을 가질지
    public DamageFieldHitType DamageFieldHitType;
    
    // 데미지 필드 지속시간
    public float DamageFieldDuration;
    
    // 데미지 필드 최대 힛트 카운트
    public  float HitMaxCount;
    
    // 미는 방향
    public Vector3 GetPushDirection(GameObject Owner, GameObject Target)
    {
        switch (_damageFieldKnockbackType)
        {
            case DamageFieldKnockbackType.Forward:
                return Owner.transform.forward;
            case DamageFieldKnockbackType.Up:
                return Vector3.up;
        }

        return Vector3.zero;
    }
}

[CreateAssetMenu(menuName = "Scriptable Object/DamageField Data Group")]
public class DamageFieldDataGroup : ScriptableObject
{
    [SerializeField]
    public SerializedDictionary<DamageFieldEnum, DamageFieldData> _damageFieldDatas_new =
        new SerializedDictionary<DamageFieldEnum, DamageFieldData>();

    [SerializeField] private SerializedDictionary<DamageFieldCollisionType, GameObject> _damageFields;
    
    public DamageFieldData GetDamageFieldData(DamageFieldEnum _damageFieldEnum)
    {
        DamageFieldData outData;
        return _damageFieldDatas_new.TryGetValue(_damageFieldEnum, out outData) ? outData : null;
    }

    public GameObject CreateDamageField(DamageFieldEnum _damageFieldEnum, GameObject owner,  Transform parent, Vector3 createPosition,
        Vector3 createDirection)

    {
        DamageFieldData damageFieldData = GetDamageFieldData(_damageFieldEnum);
        if (damageFieldData != null)
        { 
            GameObject damageFieldInstance = Instantiate(_damageFields[damageFieldData.damageFieldCollisionData.damageFieldCollisionType],createPosition, Quaternion.LookRotation(createDirection), parent);

            switch (damageFieldData.damageFieldCollisionData.damageFieldCollisionType)
            {
                case DamageFieldCollisionType.Sphere:
                {
                    SphereCollider collider = damageFieldInstance.GetComponent<SphereCollider>();
                    if (collider)
                    {
                        collider.radius = damageFieldData.damageFieldCollisionData.SphereRadius;
                    }
                } 
                    break;
                case DamageFieldCollisionType.Box:
                {
                    BoxCollider collider = damageFieldInstance.GetComponent<BoxCollider>();
                    if (collider)
                    {
                        collider.size = damageFieldData.damageFieldCollisionData.BoxSize;
                    }
                }
                    break;
                case DamageFieldCollisionType.Capsule:
                {
                    CapsuleCollider collider = damageFieldInstance.GetComponent<CapsuleCollider>();
                    if (collider)
                    {
                        collider.height = damageFieldData.damageFieldCollisionData.CapsuleHeight;
                        collider.radius = damageFieldData.damageFieldCollisionData.CapsuleRadius;
                    }
                }
                    break;
            }
            
            DamageField damageField = damageFieldInstance.GetComponent<DamageField>();
            damageField.Owner =  owner;
            damageField.DamageFieldEnum_V = _damageFieldEnum;
            return damageFieldInstance;
        }

        return null;
    }
}
