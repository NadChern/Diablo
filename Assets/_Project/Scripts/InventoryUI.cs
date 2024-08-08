using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace _Project
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ItemsStorage _itemsStorage;
        [SerializeField] private GameObject InventoryWindow;
        [SerializeField] private Transform SlotsParent;
        [SerializeField] private SlotUI SlotPrefab;

        private Dictionary<string, SlotUI> _slots = new Dictionary<string, SlotUI>();
        private GridLayoutGroup _gridLayoutGroup;

        private void Start()
        {
            InventoryWindow.SetActive(false);
            _inventory.Updated += Redraw;
        }

        private void OnDestroy() =>
            _inventory.Updated -= Redraw;


        private void Redraw()
        {
            foreach (SlotUI slot in _slots.Values)
            {
                Destroy(slot.gameObject);
            }

            _slots.Clear();

            foreach (InventoryCell cell in _inventory.Items)
            {
                if (!_slots.ContainsKey(cell.ItemId))
                {
                    SlotUI slot = Instantiate(SlotPrefab, SlotsParent);
                    Item item = _itemsStorage.GetItemById(cell.ItemId);
                    _slots[cell.ItemId] = slot;
                    slot.SetSlot(
                        new InventoryItem(item.Name, item.Sprite, item.Id),
                        _inventory, _inventory.GetItemCount(cell.ItemId));
                }
            }

            InventoryWindow.SetActive(_slots.Count > 0);
        }
    }
}
