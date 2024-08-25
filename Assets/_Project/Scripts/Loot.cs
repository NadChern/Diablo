using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    public class Loot : MonoBehaviour, IInteractable
    {
        public string Id; // item type id
       
        public void Initialize(string id)
        {
            Id = id;
        }

        public void DestroyLoot()
        {
            Destroy(gameObject);
        }
    }
}
