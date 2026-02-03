using System;
using UnityEngine;

[Serializable]
public sealed class EffectDraw : EffectPlain
{
    public int amount;

    public void Perform()
    {
       Debug.Log("draw" + amount); 
    }
}
