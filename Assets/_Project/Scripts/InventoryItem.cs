using UnityEngine;

namespace _Project
{
   
    public class InventoryItem
    {
        public string ItemName { get; private set; }
        public Sprite Icon { get; private set; }
        public string ItemID { get; private set; }

        public InventoryItem(string itemName, Sprite icon, string itemId)
        {
            ItemName = itemName;
            Icon = icon;
            ItemID = itemId;
        }
    }
}
