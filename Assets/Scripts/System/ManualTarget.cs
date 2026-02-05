using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualTarget : Singleton<ManualTarget>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask targetLayerMask;
    public GameObject arrowPrefab;

    public void StartTargeting(Vector3 startPos)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.Setup(startPos);
    }

    public Card EndTargeting(Vector3 endPos)
    {
        arrowView.gameObject.SetActive(false);
        Vector3 direction = endPos - mainCamera.transform.position;
        Ray ray = new Ray(mainCamera.transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,targetLayerMask)
            && hit.collider != null
            && hit.transform.TryGetComponent(
                out CardView cardView))
        {
            return cardView.Card;
        }

        return null;
    }

    public async UniTask<Card> ManualTargeting(Vector3 startPos)
    {
        StartTargeting(startPos);
        while (true)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                break;
            }

            await UniTask.Yield();
        }

        return EndTargeting(MouseUtils.Instance.GetMouseWorldPosition());
    }
}