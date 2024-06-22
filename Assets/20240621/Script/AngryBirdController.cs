using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdController : MonoBehaviour
{
    // 궤적 그리기
    public Transform bird;  // 발사할 새 오브젝트
    public Trajectory trajectory;  // 궤적을 그릴 Trajectory 스크립트
    public float launchForceMultiplier = 10f;  // 발사 힘의 크기를 조절하는 변수
    public float panSpeed = 0.5f;  // 카메라 이동 속도
    public float reloadTime = 2f;  // 재발사 대기 시간

    private Vector3 startPoint;  // 마우스 드래그 시작 지점
    private bool isDragging = false;  // 드래그 상태를 확인하는 플래그
    private bool isPanning = false;  // 화면 이동 상태를 확인하는 플래그
    private bool canLaunch = true;  // 발사 가능 여부를 확인하는 플래그
    private Vector3 lastPanPosition;  // 마지막 팬 위치
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼을 눌렀을 때
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0) && isDragging)  // 마우스 왼쪽 버튼을 누르고 있는 동안
        {
            ContinueDrag();
        }
        else if (Input.GetMouseButton(0) && isPanning)  // 마우스 왼쪽 버튼을 누르고 있는 동안 팬
        {
            ContinuePan();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)  // 마우스 왼쪽 버튼을 뗐을 때
        {
            ReleaseDrag();
        }
        else if (Input.GetMouseButtonUp(0) && isPanning)  // 마우스 왼쪽 버튼을 뗐을 때 팬 해제
        {
            isPanning = false;
        }
    }

    void StartDrag()
    {
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 0;  // 2D 게임이므로 z 좌표를 0으로 설정

        if (canLaunch && Vector3.Distance(startPoint, bird.position) < 1f)  // 새 근처에서 드래그 시작
        {
            isDragging = true;  // 드래그 상태로 변경
            trajectory.ClearLine();  // 기존 궤적을 초기화
        }
        else if (!canLaunch)  // 새를 재발사할 수 없는 상태
        {
            Debug.Log("발사 준비 중..");
        }
        else  // 새가 아닌 다른 곳에서 드래그 시작
        {
            isPanning = true;
            lastPanPosition = Input.mousePosition;
        }
    }

    void ContinueDrag()
    {
        Vector3 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint.z = 0;

        Vector3[] points = CalculateTrajectoryPoints(bird.position, (startPoint - currentPoint) * launchForceMultiplier, 50);
        trajectory.RenderLine(bird.position, points);  // 궤적을 렌더링
    }

    void ReleaseDrag()
    {
        isDragging = false;
        Vector3 launchDirection = startPoint - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchDirection.z = 0;

        bird.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForceMultiplier, ForceMode2D.Impulse);
        trajectory.ClearLine();  // 궤적을 초기화
        canLaunch = false;  // 새를 발사할 수 없도록 설정
        StartCoroutine(ReloadBird());  // 재발사 대기 시간 시작
    }

    void ContinuePan()
    {
        Vector3 currentPanPosition = Input.mousePosition;
        Vector3 difference = Camera.main.ScreenToWorldPoint(lastPanPosition) - Camera.main.ScreenToWorldPoint(currentPanPosition);

        Camera.main.transform.position += difference;
        lastPanPosition = currentPanPosition;
    }

    Vector3[] CalculateTrajectoryPoints(Vector3 startPosition, Vector3 velocity, int numPoints)
    {
        Vector3[] points = new Vector3[numPoints];
        float timeStep = 0.1f;  // 시간 간격
        Vector3 gravity = Physics2D.gravity;  // 중력 벡터

        for (int i = 0; i < numPoints; i++)
        {
            float time = i * timeStep;
            points[i] = startPosition + velocity * time + 0.5f * gravity * time * time;  // 물리 법칙을 이용한 궤적 계산
        }

        return points;  // 계산된 궤적 점들을 반환
    }

    IEnumerator ReloadBird()
    {
        yield return new WaitForSeconds(reloadTime);  // 재발사 대기 시간
        canLaunch = true;  // 새를 발사할 수 있도록 설정
        Debug.Log("발사 준비 완료!");
    }
    
    
    // 이전 코드 -> 안쓰는 코드
    // void Awake()
    // {
    //     _rigidbody2D = GetComponent<Rigidbody2D>();
    // }

    // void Start()
    // {
    //     // 물리가 끝난다음 다은 차레가 되면 돌아오기 위한 포지션
    //     //_initPosition = transform.position;
    // }

    // void Shot(Vector2 dir, float normalized)
    // {
    //     // 방향을 계산해서 그쪽으로 날린다.
    //     _rigidbody2D.AddForce(dir * (normalized * MaxPower), ForceMode2D.Impulse);
    //     
    //     // 땡기기 모드 종료
    //     bSnapped = false;
    // }

    // void Update()
    // {
    //     // 마우스가 클릭 됬다면 ( 0번은 == 왼쪽 1번은 오른쪽 )
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         // 마우스 포지션을 월드 좌표로 변환한다.
    //         Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         // 변환된 좌표의 마우스 포지션과 클릭된 Angry버드가 일치한다면
    //         Collider2D collider = Physics2D.OverlapPoint(worldPoint);
    //         if (collider != null && collider.gameObject == gameObject)
    //         {
    //             // 현재 클릭했던 마우스 위치를 기억하고
    //             clickMousePosition = Input.mousePosition;
    //             // 땡기기 모드에 들어간다
    //             bSnapped = true;
    //         }
    //         else
    //         {
    //             // 내가 클릭한 오브젝트가 앵그리버드 아닌데 클릭이 되었으면
    //             // 일단 그 위치를 저장
    //             clickMousePosition = Input.mousePosition;
    //             bGragging = true;
    //         }
    //     }
    //
    //     // 앵그리버드 클릭 안했는데 드래그 중이면
    //     if (bGragging)
    //     {
    //         HandleMouseInput();
    //         
    //         if (Input.GetMouseButtonUp(0))
    //         {
    //             bGragging = false;
    //         }
    //     }
    //
    //     // 땡기기 모드일때
    //     if (bSnapped)
    //     {
    //         // 처음 마우스 찍었단 좌표에서 현재 마우스 좌표를 빼면 앵그리버드가 날아가야 할 방향이 뜬다.
    //         Vector3 gap = clickMousePosition - Input.mousePosition;
    //         
    //         // 그 길이가 마우스 최대 허용범위를 넘어서면
    //         float currentGap = gap.magnitude;
    //         if (currentGap >= MaxGapSize)
    //         {
    //             // 마우스 최대 허용 범위로 바뀐다.
    //             currentGap = MaxGapSize;
    //         }
    //
    //         // gap에 방향이 있다면 -> 옆으로 처다보니까 right에다가 dir을 넣어준다.
    //         Vector2 dir = gap.normalized;
    //         if (dir != Vector2.zero)
    //             transform.right = dir;
    //
    //         // 땡기기 모드였다가 마우스를 땠을때
    //         if (Input.GetMouseButtonUp(0))
    //         {
    //             // 앵그리 버드를 날린다.
    //             Shot(dir, currentGap / MaxGapSize);
    //         }
    //     }
    // }
    
    // 카메라 화면 이동
    // private void HandleMouseInput()
    // {
    //     // When the left mouse button is pressed, capture the drag origin
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         // 첫 드래그 위치를 잡는다.
    //         dragOrigin = GetMouseWorldPosition();
    //         isDragging = true;
    //     }
    //
    //     // When the left mouse button is released, stop dragging
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         isDragging = false;
    //     }
    //
    //     // If dragging, move the camera
    //     if (isDragging)
    //     {
    //         // 현재 마우스 클릭한 3D 위치를 가져온다.
    //         Vector3 currentMousePosition = GetMouseWorldPosition();
    //         
    //         // 마우스 좌표가 전과 동일하면 diff가 0일꺼고 전과 동일하지 않으면 그만큼 이동시켜준다.
    //         Vector3 difference = dragOrigin - currentMousePosition;
    //         Camera.main.transform.position += difference;
    //     }
    // }
    //
    // private Vector3 GetMouseWorldPosition()
    // {
    //     Vector3 mousePosition = Input.mousePosition;
    //     mousePosition.z = -Camera.main.transform.position.z; // Adjust z-position for the camera's z-depth
    //     return Camera.main.ScreenToWorldPoint(mousePosition);
    // }
}
