using System.Collections.Generic;
using UnityEngine;
using _Project;


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

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlayerInteraction.ItemsStorage _itemsStorage;
    [SerializeField] private PlayerAttackSettings _playerAttackSettings;
    private List<InventoryItem> _items = new List<InventoryItem>();
    private List<InventoryItem> _equippedItems = new List<InventoryItem>(5);
    [SerializeField] private Health _health;
    // public IReadOnlyList<InventoryItem> Items => _items;
    // public IReadOnlyList<InventoryItem> EquippedItems => _equippedItems;

    // Add Item object to inventory by converting it to InventoryItem
    public void Put(Item item)
    {
        _items.Add(new InventoryItem(item.name, item.Sprite, item.Id));
    }

    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
    }

    // Equip item if fewer than 3 items are equipped
    public void Equip(InventoryItem item)
    {
        if (_equippedItems.Count < 3)
        {
            _equippedItems.Add(item);
            Item originalItem = _itemsStorage.GetItemById(item.ItemID);
            
            if (originalItem is Weapon || originalItem is Gear)
            {
                ApplyWeaponGearEffect(originalItem, true);
            }
            else if (originalItem is Potion)
            {
                ApplyPotionEffect(originalItem, true);
            }

            Remove(item);
        }
        else
        {
            Debug.Log("You can equip a maximum of 3 items.");
        }
    }


    // Unequip item, reverse its effects, add it back to inventory
    public void Unequip(InventoryItem item)
    {
        if (_equippedItems.Contains(item))
        {
            _equippedItems.Remove(item);
            Item originalItem = _itemsStorage.GetItemById(item.ItemID);

            if (originalItem is Weapon || originalItem is Gear)
            {
                ApplyWeaponGearEffect(originalItem, false);
            }
            
            _items.Add(item);
        }
    }

    private void ApplyPotionEffect(Item item, bool isEquipped)
    {
        if (item is Potion potion && isEquipped)
        {
            if (_health.CurrentHealth < _health.MaxHealth * 0.25f)
            {
                _health.Heal(potion.HealAmount);
                Debug.Log("Potion used to heal.");
                // Remove potion after use
                _equippedItems.Remove(new InventoryItem(item.name, item.Sprite, item.Id)); 
            }  
        }
    }
    private void ApplyWeaponGearEffect(Item item, bool isEquipped)
    {
        if (item is Weapon weapon)
        {
            if (isEquipped)
            {
                _playerAttackSettings.UpdateWeapon(weapon.Damage, weapon.Range, weapon.Cooldown);
            }
            else
            {
                _playerAttackSettings.ResetToDefault();
            }
        }
        else if (item is Gear gear)
        {
            // TODO Apply gear effects, 
        }
    }
}
