using System;
using CardEnum;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FieldZonesSystem : Singleton<FieldZonesSystem>
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SummonMonsterGA>(PerformMonsterSummon);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SummonMonsterGA>();
    }

    public async UniTask PerformMonsterSummon(SummonMonsterGA summonMonsterGA)
    {
        CardSystem.Instance.MoveCardLocation(summonMonsterGA.SummonedCard.GetCard(), summonMonsterGA.SummonedCardLocation);
        // effect (ETB)
        Debug.Log("Monster summoned");
    }
}