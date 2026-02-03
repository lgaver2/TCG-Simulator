using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject wrapper;
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private TMP_Text effect;

    public Card card { get; private set; }


    public void SetCard(Card card)
    {
        this.card = card;
        cardImage.sprite = card.Sprite;
        cardName.text = card.Name;
        cardCost.text = card.Cost.ToString();
    }

    public GameObject GetWrapper()
    {
        return wrapper;
    }
    
    public Card GetCard()
    {
        return card;
    }
    
}