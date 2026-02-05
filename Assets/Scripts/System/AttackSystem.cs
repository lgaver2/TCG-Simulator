using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttackSystem : Singleton<AttackSystem>
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AttackGA>(AttackPerformer);
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerfomer);
    }

    private void OnDisable()
    {
       ActionSystem.DetachPerformer<AttackGA>(); 
       ActionSystem.DetachPerformer<DealDamageGA>(); 
    }

    private async UniTask AttackPerformer(AttackGA attackGA)
    {
        Card attacker = attackGA.Attacer;
        Card defender = attackGA.Defender;
        DealDamageGA dealDamageGA = new DealDamageGA(attacker.Cost, defender.Cost);
        ActionSystem.Instance.AddAction(dealDamageGA);
        await UniTask.Yield();
    }

    private async UniTask DealDamagePerfomer(DealDamageGA dealDamageGA)
    {
       // damage calc 
       // foreach (target in targets)
       // card.damage() ...
        await UniTask.Yield();
    }
}
