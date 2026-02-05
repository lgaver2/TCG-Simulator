using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private ManaUI manaUI;
    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;

    private void OnEnable()
    {
       ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer); 
       ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer); 
       ActionSystem.SubscribeReaction<OpponentTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
       ActionSystem.DetachPerformer<SpendManaGA>(); 
       ActionSystem.DetachPerformer<RefillManaGA>();
       ActionSystem.UnSubscribeReaction<OpponentTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public bool EnoughMana(int amount)
    {
        return amount <= currentMana;
    }

    private async UniTask SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        await UniTask.Yield();
    }

    private async UniTask RefillManaPerformer(RefillManaGA spendManaGA)
    {
        currentMana = MAX_MANA;
        manaUI.UpdateManaText(currentMana);
        await UniTask.Yield();
    }


    private void EnemyTurnPostReaction(OpponentTurnGA opponentTurnGA)
    {
        RefillManaGA refillManaGa = new();
        ActionSystem.Instance.AddAction(refillManaGa);
    }
}
