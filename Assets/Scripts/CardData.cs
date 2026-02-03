using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int Cost;
    [SerializeReference]
    public List<EffectPlain> Effects;

}
