using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public int Cost;
    public string Effect;

}
