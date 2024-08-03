using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private PlayerAttackSettings _playerAttackSettings;
    [SerializeField] private Health _health;
    private ItemsStorage _itemsStorage;

    private const int MAX_EQUIP = 2; // gear and weapon

    // how to deal with multiple gears/weapons in equip, so they dont interfere each other?
    private List<InventoryItem> _items = new List<InventoryItem>();
    private List<InventoryItem> _equippedItems = new List<InventoryItem>(MAX_EQUIP);
    public IReadOnlyList<InventoryItem> EquippedItems => _equippedItems;
    public IReadOnlyList<InventoryItem> Items => _items;


    // Add Item object to inventory by converting it to InventoryItem
    public void Put(Item item)
    {
        _items.Add(new InventoryItem(item.name, item.Sprite, item.Id));
    }

    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
    }

    // Equip item if fewer than max limit items are equipped
    public void Equip(InventoryItem item)
    {
        Item originalItem = _itemsStorage.GetItemById(item.ItemID);

        if (originalItem is Potion potion)
        {
            UsePotion(potion, item);
            return;
        }

        // TODO how to deal with mult weapon and gear if they have same properties with dif values
        // Check if the item is a weapon and if a weapon is already equipped
        if (originalItem is Weapon && _equippedItems.Any(e => _itemsStorage.GetItemById(e.ItemID) is Weapon))
        {
            Debug.Log("Only one weapon can be equipped at a time.");
            return;
        }

        // Equip item if there is space and it's a weapon or gear
        if (_equippedItems.Count < MAX_EQUIP && (originalItem is Weapon || originalItem is Gear))
        {
            _equippedItems.Add(item);
            _items.Remove(item);
            ApplyWeaponGearEffect(originalItem, true);
        }
    }

    // Unequip item, reverse its effects, add it back to inventory
    public void Unequip(InventoryItem item)
    {
        if (_equippedItems.Contains(item))
        {
            _equippedItems.Remove(item);
            _items.Add(item);
            Item originalItem = _itemsStorage.GetItemById(item.ItemID);
            ApplyWeaponGearEffect(originalItem, false);
        }
    }

    private void UsePotion(Potion potion, InventoryItem item)
    {
        if (_health.CurrentHealth < _health.MaxHealth)
        {
            _health.Heal(potion.HealAmount);
            Remove(item);
        }
        else
        {
            Debug.Log("Health is not low enough to use the potion.");
        }
    }

    private void ApplyWeaponGearEffect(Item item, bool isEquipped)
    {
        if (item is Weapon weapon)
        {
            ApplyWeaponEffect(weapon, isEquipped);
        }
        else if (item is Gear gear)
        {
            ApplyGearEffect(gear, isEquipped);
        }
    }

    private void ApplyWeaponEffect(Weapon weapon, bool isEquipped)
    {

        if (isEquipped)
        {
            _playerAttackSettings.UpdateWeapon(weapon.Damage, weapon.Range, weapon.Cooldown);
        }
        else
        {
            _playerAttackSettings.ResetWeapon();
        }
    }

    private void ApplyGearEffect(Gear gear, bool isEquipped)
    {
        if (isEquipped)
        {
            _playerAttackSettings.UpdateGear(gear.Velocity, gear.DamageResistence);
        }
        else
        {
            _playerAttackSettings.ResetGear();
        }
    }
}


