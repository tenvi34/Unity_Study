using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    // 최대 게이지 파워
    public float MaxPower = 0;
    private bool bSnapped = false;

    private Vector3 clickMousePosition = Vector2.zero;
    public float MaxGapSize = 0.0f;

    private Vector3 _initPosition;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 물리가 끝난다음 다은 차레가 되면 돌아오기 위한 포지션
        _initPosition = transform.position;
    }

    void Shot(Vector2 dir, float normalized)
    {
        _rigidbody2D.AddForce(dir * (normalized * MaxPower), ForceMode2D.Impulse);
        bSnapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(worldPoint);
            if (collider != null && collider.gameObject == gameObject)
            {
                // 현재 클릭한 마우스 위치 기억
                clickMousePosition = Input.mousePosition;
                // 발사 모드
                bSnapped = true;
            }
        }

        // 발사 모드일 때
        if (bSnapped)
        {
            // 처음 마우스 찍었던 좌표에서 현재 마우스 좌표를 빼면 앵그리버드가 날아가야 할 방향이 나온다.
            Vector3 gap = clickMousePosition - Input.mousePosition;
            
            // 그 길이가 마우스 최대 허용범위를 넘어서면
            float currentGap = gap.magnitude;
            if (currentGap >= MaxGapSize)
            {
                // 마우스 최대 허용범위로 교체
                currentGap = MaxGapSize;
            }

            // gap에 방향이 있다면 -> 옆으로 보니깐 right에 dir 넣는다.
            Vector2 dir = gap.normalized;
            if (dir != Vector2.zero)
                transform.right = dir;

            // 발사 모드에서 마우스를 놓을 때
            if (Input.GetMouseButtonUp(0))
            {
                // 앵그리버드 발사 
                Shot(dir, currentGap / MaxGapSize);
            }
        }
    }
}