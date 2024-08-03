using UnityEngine;

namespace _Project
{
   public class Enemy : BotBehavior, IInteractable
   {
      [SerializeField] private GameObject lootPrefab; 
      [SerializeField] private Transform lootSpawnPoint; // Point where the loot will be spawned
      [SerializeField] private Health _health;
     
      protected override void Start()
      {
         base.Start();
         _health.OnDeath += DropLoot; // Subscribe to the OnDeath event
      }
      
     private void DropLoot()
      {
         if (lootPrefab != null && lootSpawnPoint != null)
         {
            Instantiate(lootPrefab, lootSpawnPoint.position, Quaternion.identity);
            Debug.Log("Loot dropped.");
         }
      }
  }
}
