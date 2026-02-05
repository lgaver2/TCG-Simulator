using CardEnum;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldDropArea : MonoBehaviour, IDropArea
{
    public CardLocation location;
    public bool OnCardDrop(CardView cardView)
    {
        Card droppedCard = cardView.GetCard();
        if (droppedCard.CardType is CardType.Monster)
        {
            if (droppedCard.Location is not CardLocation.BattleZone or CardLocation.FortressZone)
            {
                SummonMonsterGA summonMonsterGA = new SummonMonsterGA(cardView, location);
                ActionSystem.Instance.Perform(summonMonsterGA).Forget();
            }

            cardView.transform.position = transform.position;
        }
        return droppedCard.CardType is  CardType.Monster;
    }
}