using CardEnum;
using UnityEngine;

public class ActionDropArea : MonoBehaviour, IDropArea
{
    public bool OnCardDrop(CardView cardView)
    {
        Card dropppedCard = cardView.Card;
        if (dropppedCard.CardType is CardType.Action)
        {
            PlayCardGA playCardGA = new(cardView.Card);
            ActionSystem.Instance.Perform(playCardGA).Forget();
        }
        return dropppedCard.CardType is CardType.Action;
    }
}