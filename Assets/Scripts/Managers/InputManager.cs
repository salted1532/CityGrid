using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// --------------------------------------------------
// InputManager는 플레이어가 작동시키는 모든 인풋을 감지하고 요청을 보내는 매니저입니다.
// --------------------------------------------------

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayerMask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            OnClicked?.Invoke();
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

}
