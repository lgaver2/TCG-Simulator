using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchupSystem : MonoBehaviour
{
   [SerializeField] private List<CardData> deckList;

   private void Start()
   {
      CardSystem.Instance.Setup(deckList);
      DrawCardGA drawCardGA = new(5);
      ActionSystem.Instance.Perform(drawCardGA).Forget();
   }
}
