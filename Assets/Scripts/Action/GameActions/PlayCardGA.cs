using UnityEngine;

public class PlayCardGA : GameAction
{
    public Card Card { get; set; }
    public SummonMonsterGA SummonMonsterGA {get; set;}

    public PlayCardGA(Card card)
    {
        Card = card;
        SummonMonsterGA = null;
    }

    public PlayCardGA(Card card, SummonMonsterGA summonMonsterGA)
    {
        Card = card;
        SummonMonsterGA = summonMonsterGA;
    }
}
