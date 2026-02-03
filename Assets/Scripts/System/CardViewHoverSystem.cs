using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
   [SerializeField] private CardView cardViewHover;

   public void Show(Card card)
   {
      cardViewHover.gameObject.SetActive(true);
      cardViewHover.SetCard(card);
   }

   public void Hide()
   {
      cardViewHover.gameObject.SetActive(false);
   }
}
