using CardEnum;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldDropArea : DropArea
{
    [SerializeField] private CardLocation location;

    [SerializeField] private int zoneIndex;

    protected override bool DropCardAction(CardView cardView)
    {
        if (!FieldZonesSystem.Instance.IsZoneFree(location, zoneIndex)) return false;
        Card droppedCard = cardView.Card;
        if (droppedCard.CardType is CardType.Monster &&
            droppedCard.Location is not CardLocation.BattleZone or CardLocation.FortressZone)
        {
            SummonMonsterGA summonMonsterGA = new SummonMonsterGA(cardView, location, zoneIndex);
            PlayCardGA playCardGA = new(cardView.Card, summonMonsterGA);
            ActionSystem.Instance.Perform(playCardGA).Forget();
            cardView.transform.position = transform.position;
            return true;
        }

        return false;
    }
}