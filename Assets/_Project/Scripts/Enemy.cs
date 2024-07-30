using UnityEngine;

namespace _Project
{
   public class Enemy : BotBehavior, IInteractable
   {
      private Health _health;
     
      protected override void Start()
      {
         base.Start();
         _health = GetComponent<Health>();
       }
      
      public void OnInteract()
      {
         Debug.Log("Interacting with enemy");
      }
      
   }
}
