using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
   public void OnInteract()
   {
      Debug.Log("Interacting with enemy");
   }
}
