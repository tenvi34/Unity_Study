﻿//
//SpringBone.cs for unity-chan!
//
//Original Script is here:
//ricopin / SpringBone.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Revised by N.Kobayashi 2014/06/20
//

using UnityEngine;

namespace UnityChan
{
    public class SpringBone : MonoBehaviour
    {
        //次のボーン
        public Transform child;

        //ボーンの向き
        public Vector3 boneAxis = new(-1.0f, 0.0f, 0.0f);
        public float radius = 0.05f;

        //各SpringBoneに設定されているstiffnessForceとdragForceを使用するか？
        public bool isUseEachBoneForceSettings;

        //バネが戻る力
        public float stiffnessForce = 0.01f;

        //力の減衰力
        public float dragForce = 0.4f;
        public Vector3 springForce = new(0.0f, -0.0001f, 0.0f);
        public SpringCollider[] colliders;

        public bool debug = true;

        //Kobayashi:Thredshold Starting to activate activeRatio
        public float threshold = 0.01f;
        private Vector3 currTipPos;

        private Quaternion localRotation;

        //Kobayashi:Reference for "SpringManager" component with unitychan 
        private SpringManager managerRef;

        //Kobayashi
        private Transform org;
        private Vector3 prevTipPos;
        private float springLength;
        private Transform trs;

        private void Awake()
        {
            trs = transform;
            localRotation = transform.localRotation;
            //Kobayashi:Reference for "SpringManager" component with unitychan
            // GameObject.Find("unitychan_dynamic").GetComponent<SpringManager>();
            managerRef = GetParentSpringManager(transform);
        }

        private void Start()
        {
            springLength = Vector3.Distance(trs.position, child.position);
            currTipPos = child.position;
            prevTipPos = child.position;
        }

        private void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(currTipPos, radius);
            }
        }

        private SpringManager GetParentSpringManager(Transform t)
        {
            var springManager = t.GetComponent<SpringManager>();

            if (springManager != null)
                return springManager;

            if (t.parent != null) return GetParentSpringManager(t.parent);

            return null;
        }

        public void UpdateSpring()
        {
            //Kobayashi
            org = trs;
            //回転をリセット
            trs.localRotation = Quaternion.identity * localRotation;

            var sqrDt = Time.deltaTime * Time.deltaTime;

            //stiffness
            var force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

            //drag
            force += (prevTipPos - currTipPos) * dragForce / sqrDt;

            force += springForce / sqrDt;

            //前フレームと値が同じにならないように
            var temp = currTipPos;

            //verlet
            currTipPos = currTipPos - prevTipPos + currTipPos + force * sqrDt;

            //長さを元に戻す
            currTipPos = (currTipPos - trs.position).normalized * springLength + trs.position;

            //衝突判定
            for (var i = 0; i < colliders.Length; i++)
                if (Vector3.Distance(currTipPos, colliders[i].transform.position) <= radius + colliders[i].radius)
                {
                    var normal = (currTipPos - colliders[i].transform.position).normalized;
                    currTipPos = colliders[i].transform.position + normal * (radius + colliders[i].radius);
                    currTipPos = (currTipPos - trs.position).normalized * springLength + trs.position;
                }

            prevTipPos = temp;

            //回転を適用；
            var aimVector = trs.TransformDirection(boneAxis);
            var aimRotation = Quaternion.FromToRotation(aimVector, currTipPos - trs.position);
            //original
            //trs.rotation = aimRotation * trs.rotation;
            //Kobayahsi:Lerp with mixWeight
            var secondaryRotation = aimRotation * trs.rotation;
            trs.rotation = Quaternion.Lerp(org.rotation, secondaryRotation, managerRef.dynamicRatio);
        }
    }
}