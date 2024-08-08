using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    public class Loot : MonoBehaviour, IInteractable
    {
        public string Id;
      
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
