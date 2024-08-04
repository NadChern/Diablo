using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace _Project
{
    public class SlotUI: MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI QuantityText;
        private InventoryItem _item;
        [SerializeField] private int _quantity;

        public void SetSlot(InventoryItem item, int quantity)
        {
            _item = item;
            _quantity = quantity;
            Icon.sprite = _item.Icon;
            QuantityText.text = _quantity > 1 ? _quantity.ToString() : "";
            gameObject.SetActive(true);
        }
        
        public void ClearSlot()
        {
            _item = null;
            _quantity = 0;
            Icon.sprite = null;
            QuantityText.text = "";
            gameObject.SetActive(false);
        }
    }
}
