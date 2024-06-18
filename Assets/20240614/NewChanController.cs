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
    // 공격
    public static readonly int PunchStringHash = Animator.StringToHash("PUNCH_Generic");
    public static readonly int LeftPunchStringHash = Animator.StringToHash("PUNCH_L");
    public static readonly int RightKickStringHash = Animator.StringToHash("KICK_R");
    public static readonly int LeftKickStringHash = Animator.StringToHash("KICK_L");

    public static readonly int[] ComboAnimations = { 
        PunchStringHash,
        LeftPunchStringHash,
        RightKickStringHash,
        LeftKickStringHash,
    };
    
    public static int GetComboAnimNumber(int num) =>
        HashingValue.ComboAnimations.Length > num ? HashingValue.ComboAnimations[num] : HashingValue.ComboAnimations[^1];
}

public class NewChanController : MonoBehaviour
{
    public class AnimationWrapper
    {
        // 선언
        private NewChanController _newChanController;
        
        // 선언 (ex 이사)
        private Animator _animator;
        private Coroutine CurrentAnimationCoroutine = null;

        // 생성자 선언
        AnimationWrapper(NewChanController newChanController)
        {
            _newChanController = newChanController;
            
            // caching 
            _animator = _newChanController?.GetComponent<Animator>();
            if (!_animator)
                Debug.Log("Not Found Animator");

            CurrentAnimationCoroutine = null;
        }

        public void FixedUpdate(float vertical)
        {
            _animator?.SetFloat(HashingValue.SpeedStringHash, vertical);
        }

        public static AnimationWrapper NewAnimationWrapper(NewChanController newChanController)
        {
            return new AnimationWrapper(newChanController);
        }
        
        public void PlayAnimation(int animationNameHash, float speed = 1.0f)
        {
            // 현재 재생중인 애니메이션이 있으면 그에 대한 플레잉 여부를 코루틴으로 체크하고
            // 그 재생에 대한 옵션을 정지시키고
            if (CurrentAnimationCoroutine != null)
            {
                _newChanController.StopCoroutine(CurrentAnimationCoroutine);
                CurrentAnimationCoroutine = null;
                if (_animator != null)
                {
                    // 애니메이터의 속도는 그대로 바꿔주고
                    _animator.speed = 1.0f;
                }
            }
        
            // 새로운 애니메이션을 플레잉하는 코루틴 생성
            CurrentAnimationCoroutine = _newChanController.StartCoroutine(PlayAnimation_Internal(animationNameHash, speed));
        }

        public bool CanComboState(int checkAnimationName)
        {
            if (IsName(checkAnimationName))
            {
                var stateInfo = _animator?.GetCurrentAnimatorStateInfo(0);
                Debug.Log(stateInfo?.normalizedTime);
                // 애니메이션의 길이는 0~1인데 normalizedTime 부근을 넘어서 누르면 콤보가 가능한 상태로 인식
                return stateInfo?.normalizedTime >= 0.7;
            }

            return false;
        }

        public bool IsName(int targetAnimationHash)
        {
            var stateInfo = _animator?.GetCurrentAnimatorStateInfo(0);
            var nextStateInfo = _animator?.GetNextAnimatorStateInfo(0);
            return stateInfo?.fullPathHash == targetAnimationHash || stateInfo?.shortNameHash == targetAnimationHash 
                || nextStateInfo?.fullPathHash == targetAnimationHash || nextStateInfo?.shortNameHash == targetAnimationHash;
        }

        IEnumerator PlayAnimation_Internal(int animationNameHash, float speed)
        {
            if (_animator != null)
            {
                _animator.CrossFade(animationNameHash, 0.1f, 0);
                _animator.speed = speed;
            }
        
            yield return null;
        
            while (true)
            {
                var stateInfo = _animator?.GetCurrentAnimatorStateInfo(0);
                if (stateInfo?.fullPathHash == animationNameHash || stateInfo?.shortNameHash == animationNameHash) yield return null;
                else break;
            }

            if (_animator != null)
            {
                _animator.speed = 1;
            }
            CurrentAnimationCoroutine = null;
        }
    }
    
    // 일반형
    public float Speed = 0.0f;
    public float RotSpeedDegree = 0.0f;
    public float JumpPower = 0.0f;
    
    // 바인딩형
    public AudioClip OnHitAudioClip = null;
    public GameObject OnHitParticle = null;
    public Transform RightHandTransform;
    //public Transform LeftHandTransform;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private ParticleSystemController _particleSystemController;
    private AnimationWrapper _animationWrapper;
    
    private Coroutine ComboProcessCoroutine = null;

    //private Coroutine CurrentAnimationCoroutine = null;

    void Awake()
    {
        Debug.Log("Awake");
        
        // caching
        
        _rigidbody = GetComponent<Rigidbody>();
        if (!_rigidbody) Debug.Log("Not Found RigidBody");
        
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource) Debug.Log("Not Found AudioSource");
        if (! OnHitAudioClip) Debug.Log("OnHitAudioClip Not Bound");
        
        _particleSystemController = GetComponent<ParticleSystemController>();
        if (! _particleSystemController) Debug.Log("Not Found ParticleSystemController");
        
        _animationWrapper = AnimationWrapper.NewAnimationWrapper(this);
        if (_animationWrapper == null) Debug.Log("Creation Fail AnimationWrapper");
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
            //_animator.CrossFade(HashingValue.JumpStringHash, 0, 0, 0);
            PlayAnimation(HashingValue.JumpStringHash);
        }
        
        // 공격
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            //_animator.CrossFade(HashingValue.PunchStringHash, 0.1f, 0);
            PlayAnimation(HashingValue.PunchStringHash, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //_animator.CrossFade(HashingValue.LeftPunchStringHash, 0.1f, 0);
            PlayAnimation(HashingValue.LeftPunchStringHash);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //_animator.CrossFade(HashingValue.LeftPunchStringHash, 0.1f, 0);
            PlayAnimation(HashingValue.RightKickStringHash);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //_animator.CrossFade(HashingValue.LeftPunchStringHash, 0.1f, 0);
            PlayAnimation(HashingValue.LeftKickStringHash);
        }
        */
        
        // 콤보 공격
        ComboProcess();
    }
    
    IEnumerator ComboProcess_Loop()
    {
        int currentComboNumber = 0;
 
        // 최초에는 코루틴이 안도는 것이므로 첫번째 애니메이션을 실행한다
        PlayAnimation(HashingValue.ComboAnimations[currentComboNumber]);

        // 애니메이션 플레잉 여부를 체크하기 위해 한 프레임 기다린다.
        yield return null;
    
        // _animationWrapper가 null이 아닌경우에만 루프를 수행한다.
        while (_animationWrapper != null)
        {
            // 현재 재생중인 애니메이션이 combo애니메이션이고
            if (_animationWrapper.IsName(HashingValue.GetComboAnimNumber(currentComboNumber)))
            {
                // P를 눌렀을 때 콤보가 가능한 상태이면
                if (Input.GetKeyDown(KeyCode.P) && 
                    _animationWrapper.CanComboState(HashingValue.GetComboAnimNumber(currentComboNumber)))
                {
                    // 다음 콤보를 위해 인덱스를 증가하고
                    currentComboNumber++;
                    // 다음 콤보 애니메이션을 재생한다.
                    if (HashingValue.ComboAnimations.Length > currentComboNumber)
                        PlayAnimation(HashingValue.GetComboAnimNumber(currentComboNumber));
                }
                // while의 무한루프를 1frame씩 끊어가기 위해 yield return null을 해준다.
                yield return null;
            }
            else
            {
                // 콤보에 유효한 애니메이션이 없으므로 while을 빠져나간다.
                break;
            }
        }

        // 그리고 다음 코루틴을 정상 작동 시키기 위해 null만든다.
        ComboProcessCoroutine = null;
    }
    
    void ComboProcess()
    {
        if (ComboProcessCoroutine == null)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ComboProcessCoroutine = StartCoroutine(ComboProcess_Loop());
            }
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
        
        _animationWrapper.FixedUpdate(vertical);

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
        //                                                        , 어디에 생성할지              ,             어느 방향으로 할지
        _particleSystemController?.OnHit(OnHitParticle, null, RightHandTransform.position, transform.forward);
    }

    void PlayAnimation(int animationNameHash, float speed = 1.0f)
    {
        _animationWrapper.PlayAnimation(animationNameHash, speed);
    }

    /*
    IEnumerator PlayAnimation_Internal(int animationNameHash, float speed)
    {
        if (_animator != null)
        {
            _animator.CrossFade(animationNameHash, 0.0f, 0);
            Debug.Log("Before");
            _animator.speed = speed;
        }

        yield return null;

        while (true)
        {
            var stateInfo = _animator?.GetCurrentAnimatorStateInfo(0);
            if (stateInfo?.fullPathHash == animationNameHash || stateInfo?.shortNameHash == animationNameHash) yield return null;
            else break;
        }

        if (_animator != null)
        {
            _animator.speed = 1;
            Debug.Log("After");
        }
        CurrentAnimationCoroutine = null;
    }
    */
}
