using Unity.AI.Navigation;
using UnityEngine;

namespace _Project
{
    public class LootService
    {
        private readonly NavMeshSurface _navMeshSurface;

        public LootService(NavMeshSurface navMeshSurface)
        {
            _navMeshSurface = navMeshSurface;
        }
        
        public void Drop(GameObject gameObject, Vector3 position)
        {
            Object.Instantiate(gameObject, position, Quaternion.identity);
            
            // Bake NavMeshSurface after dropping loot
            _navMeshSurface.BuildNavMesh(); 
            Debug.Log("Loot dropped.");
        }
    }
}
