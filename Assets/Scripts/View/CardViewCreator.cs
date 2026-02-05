using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
   [SerializeField] private CardView cardViewPrefab;
   
   public CardView CreateCardView(Vector3 position, Quaternion rotation, Card card)
   {
      CardView cardView = Instantiate(cardViewPrefab,  position, rotation);
      cardView.SetCard(card);
      return cardView;
   }
}
