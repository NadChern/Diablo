using UnityEngine;
using System.Collections.Generic;

namespace _Project
{
    public class InventoryUI: MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameObject InventoryWindow;
        [SerializeField] private Transform SlotsParent;
        [SerializeField] private SlotUI SlotPrefab;

        private Dictionary<string, SlotUI> _slots = new Dictionary<string, SlotUI>();
       
        private void Start()
        {
            InventoryWindow.SetActive(false);
        }

      public void UpdateSlotUI()
        {
            foreach (SlotUI slot in _slots.Values)
            {
                Destroy(slot.gameObject);
            }
            _slots.Clear();

            foreach (InventoryItem item in _inventory.Items)
            {
                if (!_slots.ContainsKey(item.ItemID))
                {
                    SlotUI slot = Instantiate(SlotPrefab, SlotsParent);
                    _slots[item.ItemID] = slot;
                    slot.SetSlot(item, _inventory.GetItemCount(item.ItemID));
                }
            }

            InventoryWindow.SetActive(_slots.Count > 0);
        }
    }
}
