using System;
using System.Collections.Generic;
using CardEnum;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FieldZonesSystem : Singleton<FieldZonesSystem>
{
    private readonly CardView[] BattleZoneCards = new CardView[3];
    private readonly CardView[] FortressZoneCards = new CardView[3];

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SummonMonsterGA>(PerformMonsterSummon);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SummonMonsterGA>();
    }

    private async UniTask PerformMonsterSummon(SummonMonsterGA summonMonsterGA)
    {
        CardView card = summonMonsterGA.SummonedCard;
        CardLocation location = summonMonsterGA.SummonedCardLocation;
        int zoneIndex = summonMonsterGA.ZoneIndex;
        CardSystem.Instance.MoveCardLocation(card.Card, location);

        // effect (ETB)
        switch (location)
        {
            case CardLocation.BattleZone:
                BattleZoneCards[zoneIndex] = card;
                break;
            case CardLocation.FortressZone:
                FortressZoneCards[zoneIndex] = card;
                break;
            default:
                Debug.Log("Zone not implemented");
                break;
        }

        Debug.Log("Monster summoned");

        await UniTask.Yield();
    }

    public bool IsZoneFree(CardLocation location, int zoneIndex)
    {
        switch (location)
        {
            case CardLocation.BattleZone:
                return BattleZoneCards[zoneIndex] is null;
            case CardLocation.FortressZone:
                return FortressZoneCards[zoneIndex] is null;
        }

        return false;
    }
}