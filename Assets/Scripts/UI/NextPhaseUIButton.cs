using UnityEngine;

public class NextPhaseUIButton : MonoBehaviour
{
    public void OnClick()
    {
        OpponentTurnGA opponentTurnGA = new();
        ActionSystem.Instance.Perform(opponentTurnGA).Forget();
    }
}