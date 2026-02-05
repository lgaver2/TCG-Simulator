using UnityEngine;

public class CardRoyal : Card
{
    public int FortressLifePoints { get; set; }
    public int LifePoints { get; set; }
    public CardRoyal(CardData cardData) : base(cardData)
    {
        FortressLifePoints = 5;
        LifePoints = 5;
    }
}
