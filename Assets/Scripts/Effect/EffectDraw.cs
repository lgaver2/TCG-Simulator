using System;
using UnityEngine;

[Serializable]
public sealed class EffectDraw : EffectPlain
{
    [SerializeField] private int drawAmount;
    public GameAction GetGameAction()
    {
        DrawCardGA drawCardGA = new(drawAmount);
        return drawCardGA;
    }
}
