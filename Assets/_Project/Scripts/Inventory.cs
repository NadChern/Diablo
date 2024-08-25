using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Project;
using TriInspector;

public class Inventory : MonoBehaviour
{
    public event Action Updated;

    [SerializeField] private PlayerAttackSettings _playerAttackSettings;    
    [SerializeField] private Health _health;
    [SerializeField] private ItemsStorage _itemsStorage;
    [SerializeField] private string _defaultWeaponId = "6";
    [SerializeField] private WeaponVisualController _weaponVisualController;
    
    private const int MAX_EQUIP = 2; // armor + weapon
    private List<InventoryCell> _items = new();
    private List<string> _equippedItems = new(MAX_EQUIP);

    public List<string> EquippedItems => _equippedItems;
    public List<InventoryCell> Items => _items;

    private void Start()
    {
        // Equip the default weapon at the start
        Equip(_defaultWeaponId);
    }

    public void Put(string id)
    {
        _items.Add(new InventoryCell(id, 1));
        Updated?.Invoke();
    }

    public void Remove(string id)
    {
        InventoryCell cell = _items.Find(x => x.ItemId == id);
        _items.Remove(cell);
        Updated?.Invoke();
    }

    public int GetItemCount(string id) =>
        _items
            .Where(x => x.ItemId == id)
            .Sum(x => x.Count);

    public void Equip(string id)
    {
        Item originalItem = _itemsStorage.GetItemById(id);

        if (originalItem is Potion potion)
        {
            UsePotion(potion, id);
            return;
        }

        // Only one weapon/armor can be equipped at a time
        TryReplace<Weapon>(originalItem);
        TryReplace<Armor>(originalItem);
    }

   
    private void TryReplace<T>(Item origin) where T : Item
    {
        if (origin is T t)
        {
            string currentItem = _equippedItems.Find(x => _itemsStorage.GetItemById(x) is T);
            
            if (!string.IsNullOrEmpty(currentItem))
                UnequipItem(currentItem);

            EquipItem(origin.Id);
            ApplyEffect(t, true);
        }
    }

    private void EquipItem(string id)
    {
        _equippedItems.Add(id);
        Remove(id);
        
        // Check if the item is a weapon before updating the visual representation
        Item equippedItem = _itemsStorage.GetItemById(id);
        if (equippedItem is Weapon)
        {
            _weaponVisualController.EquipWeapon(id);
        }
    }

    private void UnequipItem(string id)
    {
        _equippedItems.Remove(id);
        Put(id);
        Item originalItem = _itemsStorage.GetItemById(id);
        ApplyEffect(originalItem, false);
    }


    private void UsePotion(Potion potion, string item)
    {
        if (_health.CurrentHealth < _health.MaxHealth)
        {
            _health.Heal(potion.HealAmount);
            Remove(item);
        }
    }

    private void ApplyEffect(Item item, bool isEquipped)
    {
        if (item is Weapon weapon)
        {
            ApplyEffect(weapon, isEquipped);
        }
        else if (item is Armor armor)
        {
            ApplyEffect(armor, isEquipped);
        }
    }

    private void ApplyEffect(Weapon weapon, bool isEquipped)
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

    private void ApplyEffect(Armor armor, bool isEquipped)
    {
        if (isEquipped)
        {
            _playerAttackSettings.UpdateArmor(armor.Velocity, armor.DamageResistence);
        }
        else
        {
            _playerAttackSettings.ResetArmor();
        }
    }
}
