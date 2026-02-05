using UnityEngine;

public class DropArea : MonoBehaviour
{
   public bool OnCardDrop(CardView cardView)
   {
       if (!ManaSystem.Instance.EnoughMana(cardView.Card.Cost)) return false;
       return DropCardAction(cardView);
   }

   protected virtual bool DropCardAction(CardView cardView)
   {
       return true;
   }
}
