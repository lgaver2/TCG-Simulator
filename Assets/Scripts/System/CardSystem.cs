using System;
using System.Collections.Generic;
using System.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
   [SerializeField] HandManager handManager;
   [SerializeField] private Transform deckPosition;
   [SerializeField] private Transform trashPosition;
   [SerializeField] private CardData cardData;
   private readonly List<Card> deck = new List<Card>();
   private readonly List<Card> hand = new List<Card>();
   private readonly List<Card> trash = new List<Card>();


   private void OnEnable()
   {
      ActionSystem.AttachPerformer<DrawCardGA>(DrawCardsPerformer);
      ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
      ActionSystem.SubscribeReaction<OpponentTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
   }

   private void OnDisable()
   {
      ActionSystem.DetachPerformer<DrawCardGA>();
      ActionSystem.DetachPerformer<PlayCardGA>();
      ActionSystem.UnSubscribeReaction<OpponentTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
   }

   public void Setup(List<CardData> deckList)
   {
      foreach (var cardData in deckList)
      {
         deck.Add(new Card(cardData));
      }
   }

   
   // Performers

   private async UniTask DrawCardsPerformer(DrawCardGA drawCardGA)
   {
      for (int i=0; i< drawCardGA.amount; i++)
         await DrawCard();
   }

   private async UniTask PlayCardPerformer(PlayCardGA playCardGA)
   {
      hand.Remove(playCardGA.Card);
      CardView cardView = handManager.RemoveCard(playCardGA.Card);
      await DiscardCard(cardView);
      foreach (var effect in playCardGA.Card.Effects)
      {
         PerformEffectGA performEffectGA = new(effect);
         ActionSystem.Instance.AddAction(performEffectGA);
      }
   }
   
   // reactions
   private void EnemyTurnPreReaction(OpponentTurnGA opponentTurnGA)
   {
      DrawCardGA drawCardGA = new(2);
      ActionSystem.Instance.AddAction(drawCardGA);
   }

   
   // helpers
   private async UniTask DrawCard()
   {
      Card card = deck.Draw();
      if (card == null)
         return;
      hand.Add(card);
      CardView cardView = CardViewCreator.Instance.CreateCardView(deckPosition.position, deckPosition.rotation, card);
      await handManager.AddHandCard(cardView);
   }

   private async UniTask DiscardCard(CardView cardView)
   {
      // move to trash
      var token = this.GetCancellationTokenOnDestroy();
      await cardView.transform.DOMove(trashPosition.position, 0.5f).ToUniTask(cancellationToken:token);
      Destroy(cardView.gameObject);
   }
}
