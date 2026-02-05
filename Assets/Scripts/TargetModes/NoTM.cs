using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class NoTM : TargetMode
{
    public override List<Card> GetTargets()
    {
        return null;
    }
}
