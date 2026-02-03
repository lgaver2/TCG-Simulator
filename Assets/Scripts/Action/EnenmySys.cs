using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnenmySys : MonoBehaviour
{
   private void OnEnable()
   {
     ActionSystem.AttachPerformer<OpponentTurnGA>(EnemyTurnPerformer); 
     ActionSystem.AttachPerformer<OpponentTurnGA>(EnemyTurnPerformer); 
   }

   private void OnDisable()
   {
      ActionSystem.DetachPerformer<OpponentTurnGA>(); 
      ActionSystem.DetachPerformer<OpponentTurnGA>(); 
   }

   private async UniTask EnemyTurnPerformer(OpponentTurnGA opponentTurnGA)
   {
      Debug.Log(opponentTurnGA.ToString());
      await UniTask.Delay(TimeSpan.FromSeconds(2));
      Debug.Log(opponentTurnGA.ToString());
   }
}
