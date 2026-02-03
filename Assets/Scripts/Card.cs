using UnityEngine;

public class Card
{
    private readonly CardData cardData;
    public int Cost { get; set; }
    public string Effect { get; set; }
    
    public Card(CardData cardData)
    {
      this.cardData = cardData;
      Cost = cardData.Cost;
      Effect = cardData.Effect;
    }

    public Sprite GetSprite()
    {
        return cardData.Sprite;
    }

    public string GetName()
    {
        return cardData.Name;
    }
    
    public void PerformEffect()
    {
       Debug.Log(cardData.Effect); 
    }
}
