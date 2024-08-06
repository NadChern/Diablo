using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace _Project
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] private int _quantity;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Button _removeButton;
        private InventoryItem _item;
        
         public void SetSlot(InventoryItem item, int quantity)
        {
           if (_icon == null || item == null)
            {
                Debug.LogError("SlotUI: Icon or item is not assigned.");
                return;
            }
            
            _item = item;
            _quantity = quantity;
            _icon.sprite = _item.Icon;
            _quantityText.text = _quantity > 1 ? _quantity.ToString() : "";
            gameObject.SetActive(true);
            _removeButton.onClick.AddListener(RemoveItem);
            // GetComponent<Button>().onClick.AddListener(OnSlotClick);
        }

        public void ClearSlot()
        {
            _item = null;
            _quantity = 0;
            _icon.sprite = null;
            _quantityText.text = "";
            gameObject.SetActive(false);
            _removeButton.onClick.RemoveAllListeners();
            // GetComponent<Button>().onClick.RemoveAllListeners();
        }
        public void RemoveItem()
        {
            Debug.Log($"Attempting to remove item: {_item.ItemName} from slot.");
            _inventory.Remove(_item);
        }

        public void OnSlotClick()
        {
            if (_item != null)
            {
                _inventory.Equip(_item);
            }
        }
        
    }
}
