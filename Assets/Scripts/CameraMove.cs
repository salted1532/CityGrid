using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    private float moveSpeed = 50f;
    private float smooth = 5f;
    private float edgeSize = 20f;

    private float zoomSpeed = 800f;
    private float minZoom = 1f;
    private float maxZoom = 80f;

    private float rotateSpeed = 120f;

    private Vector3 targetPos;
    private float targetZoom;

    private bool isRotating = false;
    private Vector2 lastMousePos;

    void Start()
    {
        targetPos = transform.position;
        targetZoom = transform.position.y;
    }

    void Update()
    {
        HandleKeyboardMove();
        HandleMouseEdgeMove();
        HandleZoom();
        HandleRotation();

        ApplySmoothMovement();
    }

    // --------------------------------------------------
    // 1) WASD 이동
    // --------------------------------------------------
    void HandleKeyboardMove()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) move.y += 1;
        if (Keyboard.current.sKey.isPressed) move.y -= 1;
        if (Keyboard.current.aKey.isPressed) move.x -= 1;
        if (Keyboard.current.dKey.isPressed) move.x += 1;

        // 카메라 방향 기준 이동
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Y축 성분 제거 (카메라가 기울어져 있어도 평면 이동)
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * move.y + right * move.x;

        targetPos += direction * moveSpeed * Time.deltaTime;
    }


    // --------------------------------------------------
    // 2) 마우스 화면 가장자리 이동
    // --------------------------------------------------
    void HandleMouseEdgeMove()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 move = Vector3.zero;

        // 카메라 기준 방향 가져오기
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // 평면 이동만 하도록 Y 제거
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // 화면 가장자리 감지 후 카메라 기준 방향으로 이동
        if (mousePos.y >= Screen.height - edgeSize)
            move += forward;

        if (mousePos.y <= edgeSize)
            move -= forward;

        if (mousePos.x <= edgeSize)
            move -= right;

        if (mousePos.x >= Screen.width - edgeSize)
            move += right;

        targetPos += move * moveSpeed * Time.deltaTime;
    }


    // --------------------------------------------------
    // 3) 마우스 휠 줌
    // --------------------------------------------------
    void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            targetZoom -= scroll * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
    }

    // --------------------------------------------------
    // 4) 마우스 좌클릭 드래그 회전
    // --------------------------------------------------
    void HandleRotation()
    {
        // 좌클릭 눌렀을 때 회전 시작
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isRotating = true;
            lastMousePos = Mouse.current.position.ReadValue();
        }

        // 좌클릭 떼면 회전 종료
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isRotating = false;
        }

        if (!isRotating)
            return;

        // 현재 마우스 위치
        Vector2 currentMousePos = Mouse.current.position.ReadValue();
        Vector2 delta = currentMousePos - lastMousePos;

        // 좌우 이동량만 회전 사용
        float rotateAmount = delta.x * rotateSpeed * Time.deltaTime;

        // Y축 중심 회전
        transform.Rotate(Vector3.up, rotateAmount, Space.World);

        // 다음 계산 위해 현재 마우스 위치 저장
        lastMousePos = currentMousePos;
    }

    // --------------------------------------------------
    // 5) 부드러운 이동 적용
    // --------------------------------------------------
    void ApplySmoothMovement()
    {
        // 위치 Lerp
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smooth);

        // 줌 Lerp
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetZoom, Time.deltaTime * smooth);
        transform.position = pos;
    }
}
