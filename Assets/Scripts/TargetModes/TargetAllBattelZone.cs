using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetAllBattelZone : TargetMode
{
    public override List<Card> GetTargets()
    {
        return CardSystem.Instance.BattleZone;
    }
}
