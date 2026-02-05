using UnityEngine;

public class AttackGA : GameAction
{
   public Card Attacer { get; set; }
   public Card Defender { get; set; }

   public AttackGA(Card attacer, Card defender)
   {
      Attacer = attacer;
      Defender = defender;
   }
}
