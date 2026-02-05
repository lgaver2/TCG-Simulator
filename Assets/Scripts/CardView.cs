using System;
using System.Collections.Generic;
using CardEnum;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject wrapper;
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private TMP_Text effect;

    [SerializeField] private GameObject monsterStatus;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text defense;
    [SerializeField] private TMP_Text strike;

    public Card Card { get; private set; }
    public bool canMove { get; set; } = true;


    public void SetCard(Card card)
    {
        this.Card = card;
        UpateUI(card);
        card.onUpdateUI += UpateUI;
    }

    private void UpateUI(Card card)
    {
        cardImage.sprite = card.Sprite;
        cardName.text = card.Name;
        cardCost.text = card.Cost.ToString();
        effect.text = card.Description;
        if (card.CardType == CardType.Monster)
        {
            CardMonster monsterCard = card as CardMonster;
            if (monsterCard is null)
                return;
            monsterStatus.SetActive(true);
            attack.text = monsterCard.AttackPoint.ToString();
            defense.text = monsterCard.DefensePoint.ToString();
            strike.text = monsterCard.StrikePoint.ToString();
        } 
    }
}