using TMPro;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text manaText;
    
    public void UpdateManaText(int mana)
    {
        manaText.text = mana.ToString();
    }
}
