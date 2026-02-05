using UnityEngine;

public class DealDamageGA : GameAction
{
    public int Amount;
    public int Target;

    public DealDamageGA(int amount, int target)
    {
        Amount = amount;
        Target = target;
    }
}