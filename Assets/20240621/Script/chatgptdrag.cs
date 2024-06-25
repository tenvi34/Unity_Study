using UnityEngine;
using UnityEngine.UIElements;

public class chatgptdrag : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;
    public Renderer BackgroundRenderer;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private Vector2 prevMousePositoin = Vector2.zero;
    
    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        // When the left mouse button is pressed, capture the drag origin
        if (Input.GetMouseButtonDown(0))
        {
            // 첫 드래그 위치를 잡는다.
            dragOrigin = GetMouseWorldPosition();
            prevMousePositoin = dragOrigin;
            isDragging = true;
        }

        // When the left mouse button is released, stop dragging
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // If dragging, move the camera
        if (isDragging)
        {
            // 현재 마우스 클릭한 3D 위치를 가져온다.
            Vector3 currentMousePosition = GetMouseWorldPosition();
            
            // 마우스 좌표가 전과 동일하면 diff가 0일꺼고 전과 동일하지 않으면 그만큼 이동시켜준다.
            Vector3 difference = dragOrigin - currentMousePosition;
            Camera.main.transform.position += difference;

            // 마우스 포지션 xyz를 2d좌표인 xy로 변경
            Vector2 currentMousePosition2D = new Vector2(currentMousePosition.x, currentMousePosition.y);
            // 현재 포지션에서 그전 마우스 포지션 뺀다
            Vector2 mouseDff2D = currentMousePosition2D - prevMousePositoin;

            // uv
            float newY = mouseDff2D.x / 1080.0f;
            mouseDff2D.x = mouseDff2D.y / 1920.0f;
            mouseDff2D.y = newY;
            
            Vector2 nextDiff = BackgroundRenderer.material.GetTextureOffset(MainTex) + mouseDff2D;
            
            BackgroundRenderer.material.SetTextureOffset(MainTex, nextDiff);
           
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // Adjust z-position for the camera's z-depth
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}