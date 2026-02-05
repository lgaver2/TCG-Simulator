using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;
using CardEnum;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int Cost;
    public string Description;
    
    [ShowIf("isMonster")] public int AttackPoint;
    [ShowIf("isMonster")] public int DefensePoint;
    [ShowIf("isMonster")] public int StrikePoint;
    private bool isMonster => CardType == CardType.Monster;
    
    public CardType CardType;
    [SerializeReference]
    public List<TargetEffect> Effects;
}
