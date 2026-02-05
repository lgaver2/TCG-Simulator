using System.Collections.Generic;
using Alchemy.Inspector;
using CardEnum;
using UnityEngine;

public class Card
{
    private readonly CardData cardData;
    public int Cost { get; set; }
    public CardLocation Location { get; set; }
    public string Name => cardData.Name;
    public string Description => cardData.Description;
    public Sprite Sprite => cardData.Sprite;
    public List<TargetEffect> Effects => cardData.Effects;
    public CardType CardType => cardData.CardType;

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        Cost = cardData.Cost;
        Location = CardLocation.HandZone;
    }
}