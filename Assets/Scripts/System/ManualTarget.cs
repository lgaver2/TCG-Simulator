using UnityEngine;

public class ManualTarget : Singleton<ManualTarget>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;

    public void StartTargeting(Vector3 startPos)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.Setup(startPos);
    }

    public Card EndTargeting(Vector3 endPos)
    {
        arrowView.gameObject.SetActive(false);
        if (Physics.Raycast(endPos, Vector3.forward, out RaycastHit hit, 10f, targetLayerMask)
            && hit.collider != null
            && hit.transform.TryGetComponent(out CardView cardView))
        {
            return cardView.Card;
        }
        return null;
    }
}