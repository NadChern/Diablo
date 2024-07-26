using UnityEngine;

namespace _Project
{
   public class Enemy : MonoBehaviour, IInteractable
   {
      public void OnInteract()
      {
         Debug.Log("Interacting with enemy");
      }
   }
}
