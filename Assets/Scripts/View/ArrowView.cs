using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Transform arrowHead;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Camera mainCamera;

    private Vector3 startPosition;

    public void Setup(Vector3 startPosition)
    {
        this.startPosition = startPosition;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
    }

    private void Update()
    {
        Vector3 endPosition = MouseUtils.Instance.GetMouseWorldPosition();
        
        Vector3 direction = (endPosition - startPosition).normalized;

        arrowHead.position = endPosition;
        arrowHead.right = direction;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition - direction * 0.25f);
    }

}