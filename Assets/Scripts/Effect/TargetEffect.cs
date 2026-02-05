using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class TargetEffect
{
    [SerializeReference] public Effect effect;
    [SerializeReference] public TargetMode targetMode;
}