using CardEnum;
using UnityEngine;

public class ActionDropArea : DropArea
{
    [SerializeField] private Transform castLocation;

    protected override bool DropCardAction(CardView cardView)
    {
        Card dropppedCard = cardView.Card;
        if (dropppedCard.CardType is CardType.Action)
        {
            foreach (var effect in cardView.Card.Effects)
            {
                if (effect.targetMode == TargetModeEnum.Manual)
                {
                    // for manual target check if the target exist before casting
                    if (!ManualTarget.Instance.IsTargetExist(effect.manualTarget))
                    {
                        return false;
                    }

                    cardView.transform.position = castLocation.position;
                }
            }

            PlayCardGA playCardGA = new(cardView.Card);
            ActionSystem.Instance.Perform(playCardGA).Forget();
        }

        return dropppedCard.CardType is CardType.Action;
    }
}