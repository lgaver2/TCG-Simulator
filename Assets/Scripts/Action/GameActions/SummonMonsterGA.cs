using CardEnum;
using UnityEngine;

public class SummonMonsterGA : GameAction
{
    public CardView SummonedCard { get; set; }
    public CardLocation SummonedCardLocation { get; set; }
    public int ZoneIndex { get; set; }

    public SummonMonsterGA(CardView summonedCard, CardLocation summonedCardLocation, int zoneIndex)
    {
        SummonedCard = summonedCard;
        SummonedCardLocation = summonedCardLocation;
        ZoneIndex = zoneIndex;
    }
}