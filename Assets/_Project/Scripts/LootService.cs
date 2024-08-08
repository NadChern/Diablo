using Unity.AI.Navigation;
using UnityEngine;

namespace _Project
{
    public class LootService
    {
        private readonly ItemsStorage _itemsStorage;

        private readonly NavMeshSurface _navMeshSurface;

        public LootService(NavMeshSurface navMeshSurface, ItemsStorage itemsStorage)
        {
            _itemsStorage = itemsStorage;
            _navMeshSurface = navMeshSurface;
        }

        public void Drop(string id, Vector3 position)
        {
            Item item = _itemsStorage.GetItemById(id);
            Loot loot = Object.Instantiate(item.Loot, position, Quaternion.identity);
            loot.Initialize(id); // Initialize the loot with its ID

            // Bake NavMeshSurface after dropping loot
            _navMeshSurface.BuildNavMesh();
            Debug.Log($"Loot with ID {id} dropped.");
        }
    }
}
