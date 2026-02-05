using CardEnum;
using UnityEngine;

public class SummonMonsterGA : GameAction
{
    public CardView SummonedCard { get; set; }
    public CardLocation SummonedCardLocation { get; set; }

    public SummonMonsterGA(CardView summonedCard, CardLocation summonedCardLocation)
    {
        SummonedCard = summonedCard;
        SummonedCardLocation = summonedCardLocation;
    }
}