using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashingValue
{
    // StringToHash를 해줌으로써 비교연산에서 빠르게 비교할 수 있으며
    // table에서 값을 가져올때 빠르게 가져올 수 있다.
    
    // readonly는 초기화 한번 할때만 변수를 셋팅할 수 있고 그 다음부터는 못하게 한다
    public static readonly int SpeedStringHash = Animator.StringToHash("Speed");
    public static readonly int JumpStringHash = Animator.StringToHash("JUMP00_Generic");
    public static readonly int PunchStringHash = Animator.StringToHash("PUNCH_Generic");
}

public class NewChanController : MonoBehaviour
{
    // 일반형
    public float Speed = 0.0f;
    public float RotSpeedDegree = 0.0f;
    public float JumpPower = 0.0f;
    
    // 바인딩형
    public AudioClip OnHitAudioClip = null;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    void Awake()
    {
        Debug.Log("Awake");
        
        // caching
        _animator = GetComponent<Animator>();
        if (!_animator) Debug.Log("Not Found Animator");
        _rigidbody = GetComponent<Rigidbody>();
        if (!_rigidbody) Debug.Log("Not Found RigidBody");
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource) Debug.Log("Not Found AudioSource");
        if (! OnHitAudioClip) Debug.Log("OnHitAudioClip Not Bound");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody?.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            _animator.CrossFade(HashingValue.JumpStringHash, 0, 0, 0);
        }
        
        // 공격
        if (Input.GetKeyDown(KeyCode.K))
        {
            _animator.CrossFade(HashingValue.PunchStringHash, 0.1f, 0 );
        }
    }

    private void FixedUpdate()
    {
        if (vertical != 0.0f)
        {
            float delta = vertical * Speed * Time.fixedDeltaTime;
            Vector3 curPos = transform.position;
            Vector3 farPos = transform.position + transform.forward * 100.0f;
            Vector3 nextPos = Vector3.MoveTowards(curPos, farPos, delta);
        
            _rigidbody.MovePosition(nextPos);
        }
    
        _animator?.SetFloat(HashingValue.SpeedStringHash, vertical);

        if (horizontal != 0.0f)
        {
            float delta = horizontal * RotSpeedDegree * Time.fixedDeltaTime;
            Quaternion curQuat = Quaternion.LookRotation(transform.forward);
            Quaternion rightQuat = Quaternion.LookRotation(transform.right);
            Quaternion nextQuat = Quaternion.RotateTowards(curQuat, rightQuat, delta);
        
            _rigidbody.MoveRotation(nextQuat);
        }
    }

    public void OnHit()
    {
        // PlayOneShot() -> 일회성
        _audioSource?.PlayOneShot(OnHitAudioClip);
    }
}
