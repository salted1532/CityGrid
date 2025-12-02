using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    private float moveSpeed = 50f;
    private float smooth = 5f;

    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            move.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move.y -= 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move.x += 1;
        }

        Vector3 direction = new Vector3(move.x, 0, move.y);
        targetPos += direction * moveSpeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smooth);
    }
}
