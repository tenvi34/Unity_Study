using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;

public enum DamageFieldKnockbackType
{
    None,
    Forward,
    Up,
}

public enum DamageFieldCollisionType
{
    None,
    Sphere,
    Box,
    Capsule,
}

[Serializable]
public class DamageFieldData
{
    // 데미지 필드 이름
    //public DamageFieldEnum damageFieldEnum;

    //public GameObject damageField;
    
    // 히트 타입
    public DamageFieldHitType damageFieldHitType;

    public DamageFieldCollisionType damageFieldCollisionType;
    
    public Vector3 BoxSize;
    public float SphereRadius;
    public float CapsuleRadius;
    public float CapsuleHeight;
    
    // 데미지 필드 지속시간
    public float damageFieldDuration;
    // 데미지 필드 최대 히트 카운트
    public float hitMaxCount;
    
    // 미는 힘
    public float power;
    
    [SerializeField] 
    private DamageFieldKnockbackType damageFieldKnockbackType;
    // 미는 방향
    public Vector3 GetPushDirection(GameObject owner, GameObject target)
    {
        switch (damageFieldKnockbackType)
        {
            case DamageFieldKnockbackType.Forward:
                return owner.transform.forward;
            case DamageFieldKnockbackType.Up:
                return Vector3.up;
        }
        return Vector3.zero;
    }
}

[CreateAssetMenu(menuName = "Scriptable Object/DamageField Data Group")]
//public class DamageFieldDataGroup : ScriptableObject ,ISerializationCallbackReceiver
public class DamageFieldDataGroup : ScriptableObject
{
    // // 인스펙터 상에서 damageField를 설정하는 변수, 인스펙터에서만 사용
    // [SerializeField]
    // public List<DamageFieldData> damageFieldList = new List<DamageFieldData>();
    //
    // // 실제로 데이터가 가공되어 들어가는 변수
    // private Dictionary<DamageFieldEnum, DamageFieldData> damageFieldDatas = new Dictionary<DamageFieldEnum, 
    //     DamageFieldData>();

    [SerializeField]
    public SerializedDictionary<DamageFieldEnum, DamageFieldData> damageFieldDatas_new =
        new SerializedDictionary<DamageFieldEnum, DamageFieldData>();

    [SerializeField] 
    private SerializedDictionary<DamageFieldCollisionType, GameObject> damageFields;
    
    public DamageFieldData GetDamageFieldData(DamageFieldEnum damageFieldEnum)
    {
        DamageFieldData outData;
        return damageFieldDatas_new.TryGetValue(damageFieldEnum, out outData) ? outData : null;
    }

    public GameObject CreateDamageField(DamageFieldEnum _damageFieldEnum, GameObject owner,  Transform parent, Vector3 createPosition, Vector3 createDirection)
    {
        DamageFieldData damageFieldData = GetDamageFieldData(_damageFieldEnum);
        if (damageFieldData != null)
        { 
            GameObject damageFieldInstance = 
                Instantiate(damageFields[damageFieldData.damageFieldCollisionType], createPosition, Quaternion.LookRotation(createDirection), parent);

            switch (damageFieldData.damageFieldCollisionType)
            {
                case DamageFieldCollisionType.Sphere:
                {
                    SphereCollider collider = damageFieldInstance.GetComponent<SphereCollider>();
                    if (collider)
                    {
                        collider.radius = damageFieldData.SphereRadius;
                    }
                } 
                    break;
                case DamageFieldCollisionType.Box:
                {
                    BoxCollider collider = damageFieldInstance.GetComponent<BoxCollider>();
                    if (collider)
                    {
                        collider.size = damageFieldData.BoxSize;
                    }
                }
                    break;
                case DamageFieldCollisionType.Capsule:
                {
                    CapsuleCollider collider = damageFieldInstance.GetComponent<CapsuleCollider>();
                    if (collider)
                    {
                        collider.height = damageFieldData.CapsuleHeight;
                        collider.radius = damageFieldData.CapsuleRadius;
                    }
                }
                    break;
            }
            
            DamageField damageField = damageFieldInstance.GetComponent<DamageField>();
            damageField.Owner =  owner;
            damageField.DamageFieldEnumValue = _damageFieldEnum;
            return damageFieldInstance;
        }

        return null;
    }

    // public void OnBeforeSerialize()
    // {
    //     
    // }
    //
    // public void OnAfterDeserialize()
    // {
    //     damageFieldDatas.Clear();
    //     foreach (var damageFieldData in damageFieldList)
    //     {
    //         damageFieldDatas.Add(damageFieldData.damageFieldEnum, damageFieldData);
    //     }
    // }
}

