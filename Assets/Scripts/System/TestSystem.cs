using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
   [SerializeField] private HandManager handManager;
   public CardData cardData;

   [Button]
   public void DrawCard()
   {
      CardView cardView = CardViewCreator.Instance.CreateCardView(transform.position, transform.rotation, new Card(cardData));
      handManager.AddHandCard(cardView).Forget();
   }
}
