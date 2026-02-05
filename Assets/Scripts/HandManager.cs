using System;
using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class HandManager : MonoBehaviour
{
   [SerializeField] private int maxhandSize;
   [SerializeField] private SplineContainer splineContainer;
   [SerializeField] private Transform cardSpawnPoint;
   [SerializeField] private CardData _cardData;
   [SerializeField] private GameObject cardView;
   private List<CardView> handCards = new();

   private void Start()
   {
   }




   public async UniTask AddHandCard(CardView card)
   {
      handCards.Add(card);
      await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
      UpdateCardPosition();
   }

   public CardView RemoveCard(Card card)
   {
      CardView cardView = GetCardView(card);
      if (cardView == null) return null;
      handCards.Remove(cardView);
      UpdateCardPosition();
      return cardView;
   }

   private CardView GetCardView(Card card)
   {
      return handCards.Where(cardView => cardView.card == card).First();
   }
   
   private void UpdateCardPosition()
   {
      if (handCards.Count == 0) return;
      float cardSpacing = 1f/maxhandSize;
      float firstCardPosition = 0.5f - (handCards.Count - 1) *cardSpacing / 2;
      Spline spline = splineContainer.Spline;
      for (int i = 0; i < handCards.Count; i++)
      {
         float p = firstCardPosition + i * cardSpacing;
         Vector3 splinePosition = spline.EvaluatePosition(p);
         Vector3 forward = spline.EvaluateTangent(p);
         Vector3 up = spline.EvaluateUpVector(p);
         Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
         handCards[i].transform.DOMove(splinePosition + 0.1f * i * Vector3.back, 0.5f);
         handCards[i].transform.DORotateQuaternion(rotation, 0.25f);
      }
   }
}
