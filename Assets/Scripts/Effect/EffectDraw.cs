using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class EffectDraw : Effect
{
    [SerializeField] private int drawAmount;
    public GameAction GetGameAction(List<Card> targets)
    {
        DrawCardGA drawCardGA = new(drawAmount);
        return drawCardGA;
    }
}
