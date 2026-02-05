using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using CardEnum;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public sealed class EffectContainer
{
    [SerializeReference] public Effect effect;
    public TargetModeEnum targetMode;
    [ShowIf("isAutoTarget")][SerializeReference] public TargetMode autoTarget;
    [ShowIf("isManualTarget")][SerializeReference] public TargetSpecification manualTarget;

    private bool isAutoTarget => targetMode == TargetModeEnum.Auto;
    private bool isManualTarget => targetMode == TargetModeEnum.Manual;
}