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


    [SerializeReference] private List<EffectPlain> effects;


    private int _layerOrder;

    public Card card { get; private set; }


    public void SetCard(Card card)
    {
        this.card = card;
        cardImage.sprite = card.GetSprite();
        cardName.text = card.GetName();
        cardCost.text = card.Cost.ToString();
        effect.text = card.Effect;
    }

    public int LayerOrder
    {
        get { return _layerOrder; }
        set
        {
            cardImage.sortingOrder = value;
            _layerOrder = value;
        }
    }

    public void PerformEffect()
    {
        foreach (var effect in effects)
        {
            effect.Perform();
        }
    }
    public GameObject GetWrapper()
    {
        return wrapper;
    }
    
    public Card GetCCrd()
    {
        return card;
    }
    
}