using UnityEngine;

public class PlayCardMonsterGA : PlayCardGA
{
    public PlayCardMonsterGA(Card card, SummonMonsterGA summonMonsterGA) : base(card)
    {
       SummonMonsterGA = summonMonsterGA; 
    }
}
