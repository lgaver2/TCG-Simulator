using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA : GameAction
{
    public Effect Effect { get; set; }
    public List<Card> Targets { get; set; }

    public PerformEffectGA(Effect effect, List<Card> targets)
    {
        Effect = effect;
        Targets = targets == null ? null :  new (targets);
    }
}
