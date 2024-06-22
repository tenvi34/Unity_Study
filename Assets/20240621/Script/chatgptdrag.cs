using UnityEngine;

public class chatgptdrag : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;

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
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // Adjust z-position for the camera's z-depth
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}