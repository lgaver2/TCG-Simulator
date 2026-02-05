using CardEnum;
using UnityEngine;

public class ActionDropArea : MonoBehaviour, IDropArea
{
    public bool OnCardDrop(CardView cardView)
    {
        Card dropppedCard = cardView.GetCard();
        if (dropppedCard.CardType is CardType.Action)
        {
            PlayCardGA playCardGA = new(cardView.GetCard());
            ActionSystem.Instance.Perform(playCardGA).Forget();
        }
        return dropppedCard.CardType is CardType.Action;
    }
}