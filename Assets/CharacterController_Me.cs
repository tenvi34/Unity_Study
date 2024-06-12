using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class MyCharacterController : MonoBehaviour
{
    public float BlendTime = 0.0f;
    
    public float MoveSpeed = 5.0f;
    public float RotDegreeSeconds = 180.0f;
    public float jumpPower = 10.0f;

    [field: SerializeField] public float CurrentHp { get; private set; }


    [field: SerializeField] public float MaxHp { get; private set; }

    private bool isGrounded = true;

    // 이동하는 방향에 대한 변수
    private Vector3 MoveDirection;

    // 회전하는 방향에 대한 변수
    private Vector3 RotDirection;

    private float startTime;

    public float MoveSpeedFacter { private get; set; }

    private void Awake()
    {
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        MoveSpeedFacter = 1;

        MaxHp = 500;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Awake();
        CurrentHp = 500;
    }

    // Update is called once per frame
    private void Update()
    {
        Awake();

        var Vertical = Input.GetAxis("Vertical");
        var Horizontal = Input.GetAxis("Horizontal");

        MoveDirection = transform.forward * Vertical;
        RotDirection = transform.right * Horizontal;

        if (Vertical != 0.0f)
            GetComponent<Animator>().SetFloat("Speed", Vertical);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>()
                .AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }

        // 체력 감소 및 회복
        if (Input.GetKeyDown(KeyCode.P)) CurrentHp -= 10;
        if (Input.GetKeyDown(KeyCode.O)) CurrentHp += 20;
        
        // 공격
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //GetComponent<Animator>().Play("Punching");
            //GetComponent<Animator>().SetTrigger("Punching");
            GetComponent<Animator>().CrossFade("Punching", BlendTime, -1, 0f);
        }

        // 벡터 정규화
        MoveDirection.Normalize();
        RotDirection.Normalize();

        // if (Input.GetKeyDown(KeyCode.K))
        //     // 서버에 데이터 전송
        //     StartCoroutine(GetRequest("localhost:3000/hello?name=Hi"));
    }

    private void FixedUpdate()
    {
        // 매번 Rigidbody의 함수를 쓰는 것은 퍼포먼스 저하 발생
        // 움직임에 대한 값이 있을 때만 설정
        if (MoveDirection != Vector3.zero)
        {
            // var nextPosition = Vector3.MoveTowards(transform.position, transform.position + MoveDirection * 1000.0f,
            //     Time.fixedDeltaTime * MoveSpeed * MoveSpeedFacter);

            var nextPosition = Vector3.MoveTowards(transform.position,
                transform.forward + MoveDirection * 1000.0f,
                MoveSpeed * MoveSpeedFacter * Time.fixedDeltaTime);

            // fixedDeltaTime == 0.02초 프로젝트 설정
            // 50번 불리니까
            // fixedDeltaTime * MoveSpeed = 1tick당 이동할 거리
            //                거리
            //           ---------------
            //            속력  |  시간

            GetComponent<Rigidbody>().MovePosition(nextPosition);
        }

        // 회전
        if (RotDirection != Vector3.zero)
        {
            // var nextRotaton =
            //     Vector3.RotateTowards(transform.forward, RotDirection, Time.fixedDeltaTime * RotDegreeSeconds, 0.0f);
            // GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(nextRotaton));

            // Quaternion.LookRotation은 Vector3를 Quaternion로 변환
            var newQuat = Quaternion.RotateTowards(
                Quaternion.LookRotation(transform.forward),
                Quaternion.LookRotation(RotDirection),
                RotDegreeSeconds * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MoveRotation(newQuat);
        }
    }

    // 컴포넌트의 gameObject의 Collision이 다른 물체와 충돌시 Collision에 충돌 판정 발생
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.CompareTag("Jewelry"))
        {
            other.gameObject.GetComponent<ScoreEffectScript>().OnHit();
            CurrentHp -= 50;
            Debug.Log(CurrentHp);
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody>()
            .AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    // 서버에 request 부르는 함수 호출
    private IEnumerator GetRequest(string url)
    {
        // webRequest 객체 생성
        using (var webRequest = UnityWebRequest.Get(url))
        {
            // 서버로 전송
            yield return webRequest.SendWebRequest();

            // 연결 실패 시 호출
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                Debug.Log("Connection Error");
            // 연결 성공 시 호출
            else
                // 서버에서 내려온 텍스트 출력
                Debug.Log(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator TestCoroutine(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            Debug.Log("Test Success");
        }
    }

    private async void TestRun()
    {
        await Task.Run(() => TestAsync());

        Debug.Log("AsyncFinish");
    }

    private void TestAsync()
    {
        for (long i = 0; i < 50000000000; i++)
        {
        }
    }

    private void TestAsyncRun()
    {
        startTime = Time.realtimeSinceStartup;

        TestRun();

        Debug.Log(Time.realtimeSinceStartup - startTime);
    }
}