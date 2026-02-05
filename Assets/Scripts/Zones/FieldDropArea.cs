using CardEnum;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldDropArea : MonoBehaviour, IDropArea
{
    [SerializeField] private CardLocation location;

    [SerializeField] private int zoneIndex;

    public bool OnCardDrop(CardView cardView)
    {
        // is the area full?
        if (!FieldZonesSystem.Instance.IsZoneFree(location, zoneIndex)) return false;
        Card droppedCard = cardView.Card;
        if (droppedCard.CardType is CardType.Monster &&
            droppedCard.Location is not CardLocation.BattleZone or CardLocation.FortressZone)
        {
            SummonMonsterGA summonMonsterGA = new SummonMonsterGA(cardView, location, zoneIndex);
            ActionSystem.Instance.Perform(summonMonsterGA).Forget();
            cardView.transform.position = transform.position;
            return true;
        }

        return false;
    }
}