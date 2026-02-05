using System;
using System.Collections.Generic;
using CardEnum;
using Cysharp.Threading.Tasks;
using StaticUtils;
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
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayerMask)
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
        Card targetCard = null;
        while (true)
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

            targetCard = EndTargeting(MouseUtils.Instance.GetMouseWorldPosition());
            if (targetCard != null)
                break;
            await UniTask.Yield();
        }

        return targetCard;
    }


    public bool IsTargetExist(TargetSpecification spec)
    {
        foreach (var location in spec.cardLocation)
        {
            List<Card> locationCards = null;
            
            // check card depending on location
            switch (location)
            {
                case CardLocation.BattleZone:
                    locationCards = CardSystem.Instance.BattleZone;
                    break;
                case CardLocation.FortressZone:
                    locationCards = CardSystem.Instance.FortressZone;
                    break;
                case CardLocation.DropZone:
                    locationCards = CardSystem.Instance.DropZone;
                    break;
                default:
                    Debug.Log("Implement this location for targeting");
                    break;
            }

            if (locationCards == null || locationCards.Count == 0)
                continue;

            // in this location, is the card exist?
            foreach (var card in locationCards)
            {
                if (spec.cardType is { Count: > 0 })
                {
                    bool found = false;
                    foreach (var cardType in spec.cardType)
                    {
                        if (card.CardType == cardType)
                        {
                            found = true;
                        }
                    }

                    if (!found)
                        continue;
                }

                if (spec.cardCost is { Count: > 0 })
                {
                    bool found = false;
                    foreach (var costComparison in spec.cardCost)
                    {
                        if (MathUtils.Compare(card.Cost, costComparison.cost, costComparison.op))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                        continue;
                }
                return true;
            }
        }

        return false;
    }
}