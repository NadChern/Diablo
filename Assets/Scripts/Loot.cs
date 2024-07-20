using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IInteractable
{
   public void OnInteract()
   {
      Debug.Log("Interacting with Loot");
   }
}
