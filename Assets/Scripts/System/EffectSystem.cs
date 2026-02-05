using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    private void OnDisable()
    {
       ActionSystem.DetachPerformer<PerformEffectGA>(); 
    }

    // performers
    public async UniTask PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        // TODO add the targets
        GameAction effectAction = performEffectGA.Effect.GetGameAction(null);
        ActionSystem.Instance.AddAction(effectAction);
        await UniTask.Yield();
    }
}
