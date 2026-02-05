using System;
using System.Collections.Generic;
using System.Data;
using CardEnum;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
   [SerializeField] HandManager handManager;
   [SerializeField] private Transform deckPosition;
   [SerializeField] private Transform dropPosition;
   [SerializeField] private CardData cardData;
   
   private readonly List<Card> deck = new List<Card>();
   private readonly List<Card> handZone = new List<Card>();
   public List<Card> BattleZone { get; private set; }= new List<Card>();
   public List<Card> FortressZone { get; set; }= new List<Card>();
   private readonly List<Card> dropZone = new List<Card>();


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
         deck.Add(CardFactory(cardData));
      }
   }

   private Card CardFactory(CardData cardData)
   {
      switch (cardData.CardType)
      {
         case CardType.Monster:
            return new CardMonster(cardData);
         case CardType.Action:
            return new CardAction(cardData);
         case CardType.Royal:
            return new CardRoyal(cardData);
         default:
            return new  Card(cardData);
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
      handZone.Remove(playCardGA.Card);
      CardView cardView = handManager.RemoveCard(playCardGA.Card);
      SpendManaGA spendManaGA = new SpendManaGA(playCardGA.Card.Cost);
      ActionSystem.Instance.AddAction(spendManaGA);
      await DiscardCard(cardView);
      foreach (var targetEffect in playCardGA.Card.Effects)
      {
         PerformEffectGA performEffectGA = new(targetEffect.effect, targetEffect.targetMode.GetTargets());
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
      handZone.Add(card);
      CardView cardView = CardViewCreator.Instance.CreateCardView(deckPosition.position, deckPosition.rotation, card);
      await handManager.AddHandCard(cardView);
   }

   private async UniTask DiscardCard(CardView cardView)
   {
      // move to trash
      var token = this.GetCancellationTokenOnDestroy();
      await cardView.transform.DOMove(dropPosition.position, 0.5f).ToUniTask(cancellationToken:token);
      Destroy(cardView.gameObject);
   }

   public void MoveCardLocation(Card card, CardLocation newLocation)
   {
      CardLocation oldLocation = card.Location;
      card.Location = newLocation;
      switch (oldLocation)
      {
        case CardLocation.HandZone:
           handManager.RemoveCard(card);
           handZone.Remove(card);
           break;
        case CardLocation.BattleZone:
           BattleZone.Remove(card);
           break;
        case CardLocation.FortressZone:
           FortressZone.Remove(card);
           break;
        case CardLocation.DropZone:
           dropZone.Remove(card);
           break;
        default:
           Debug.Log("error zone not registerd");
           break;
      }
      
      switch (newLocation)
      {
        case CardLocation.HandZone:
           handZone.Add(card);
           break;
        case CardLocation.BattleZone:
           BattleZone.Add(card);
           break;
        case CardLocation.FortressZone:
           FortressZone.Add(card);
           break;
        case CardLocation.DropZone:
           dropZone.Add(card);
           break;
        default:
           Debug.Log("error zone not registerd");
           break;
      }
   }
}
