using UnityEngine.UI;
using TMPro;
using TriInspector;
using UnityEngine;

namespace _Project
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _equipButton;
        [SerializeField, ReadOnly] private int _quantity;

        private InventoryItem _item;
        private Inventory _inventory;

        public void SetSlot(InventoryItem item, Inventory inventory, int quantity)
        {
            _inventory = inventory;
            if (_icon == null || item == null)
            {
                return;
            }

            _item = item;
            _quantity = quantity;
            _icon.sprite = _item.Icon;
            _quantityText.text = _quantity > 1 ? _quantity.ToString() : "";
            gameObject.SetActive(true);
            _removeButton.onClick.AddListener(Remove);
            _equipButton.onClick.AddListener(Use);
        }

        private void Use() =>
            _inventory.Equip(_item.ItemID);

        public void ClearSlot()
        {
            _item = null;
            _quantity = 0;
            _icon.sprite = null;
            _quantityText.text = "";
            gameObject.SetActive(false);
            _removeButton.onClick.RemoveAllListeners();
        }

        private void Remove()
        {
            _inventory.Remove(_item.ItemID);
        }
    }
}
