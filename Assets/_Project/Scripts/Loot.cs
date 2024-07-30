using UnityEngine;

namespace _Project
{
    public class Loot : MonoBehaviour, IInteractable
    {
        // [SerializeField] private InventoryItem item;

        public void OnInteract()
        {
            Debug.Log("Interacting with Loot");
            CollectLoot();
        }

        private void CollectLoot()
        {
            Debug.Log("Loot collected");

            // Inventory playerInventory = FindObjectOfType<Inventory>();
            // Health playerHealth = FindObjectOfType<Health>();
            //
            // if (playerInventory != null && item != null)
            // {
            //     playerInventory.AddItem(item);
            // }
            //
            // if (playerHealth != null && item.healAmount > 0)
            // {
            //     playerHealth.Heal(item.healAmount);
            // }

            Destroy(gameObject);
        }
    }
}
