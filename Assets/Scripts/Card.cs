using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private readonly CardData cardData;
    public int Cost { get; set; }

    public string Name => cardData.Name;
    public Sprite Sprite => cardData.Sprite; 
    public List<EffectPlain> Effects => cardData.Effects;
    
    public Card(CardData cardData)
    {
      this.cardData = cardData;
      Cost = cardData.Cost;
    }
}
