using UnityEngine;
using UnityEngine.EventSystems;

public class FieldDropArea : MonoBehaviour, IDropArea
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCardDrop(CardView cardView)
    {
        cardView.transform.position = transform.position;
        cardView.PerformEffect();
    }
}
