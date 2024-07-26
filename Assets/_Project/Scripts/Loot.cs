using UnityEngine;

namespace _Project
{
   public class Loot : MonoBehaviour, IInteractable
   {
      public void OnInteract()
      {
         Debug.Log("Interacting with Loot");
      }
   }
}
