using UnityEngine;

public class CardMonster : Card
{
    public int AttackPoint { get; set; }
    public int DefensePoint { get; set; }
    public int StrikePoint { get; set; }

    public CardMonster(CardData cardData) : base(cardData)
    {
        AttackPoint = cardData.AttackPoint;
        DefensePoint = cardData.DefensePoint;
        StrikePoint = cardData.StrikePoint;
    }
}