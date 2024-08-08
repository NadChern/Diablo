using System.Linq;
using UnityEngine;


namespace _Project
{
    
    [CreateAssetMenu(fileName = "ItemsStorage", menuName = "Inventory/ItemsStorage")]
   public class ItemsStorage : ScriptableObject
    {
        [SerializeField] private Item[] _items;

        public Item GetItemById(string byId)
        {
            return _items.First(x => x.Id == byId);
        }
        
        public string[] GetAllItemIds()
        {
            return _items.Select(item => item.Id).ToArray();
        }
    }
}
