using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectShowMonster : Effect
{
    public GameAction GetGameAction(List<Card> targets)
    {
        foreach (var target in targets)
        {
            if (target is CardMonster targetMonster)
            {
                targetMonster.AttackPoint += 1;
            }
        }

        return new NullGA();
    }
    
}
