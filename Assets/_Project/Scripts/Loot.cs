using UnityEngine;

namespace _Project
{
    public class Loot : MonoBehaviour, IInteractable
    {
        public string Id;

        public void DestroyLoot()
        {
            Destroy(gameObject);
        }
    }
}
