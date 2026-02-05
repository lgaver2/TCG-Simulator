using UnityEngine;
using UnityEngine.InputSystem;

public class MouseUtils : Singleton<MouseUtils>
{
    [SerializeField] private Camera mainCamera;
    public Vector3 GetMouseWorldPosition()
    {
        if (Mouse.current == null) return Vector3.zero;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        float zDistance = Mathf.Abs(mainCamera.transform.position.z);

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, zDistance));

        mouseWorldPos.z = 0f;

        return mouseWorldPos;
    }
}
