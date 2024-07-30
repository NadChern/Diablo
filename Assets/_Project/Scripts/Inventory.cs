using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using _Project;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public Transform inventoryPanel; // Reference to the inventory UI panel
    public GameObject inventorySlotPrefab; // Prefab for an inventory slot

    private Health _playerHealth;
    private InventoryItem _equippedWeapon; // Store the currently equipped weapon

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
        UpdateInventoryUI();
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        UpdateInventoryUI();
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        // Clear existing slots
        foreach (Transform item in inventoryPanel)
        {
            Destroy(item.gameObject);
        }

        // Add new slots
        foreach (InventoryItem item in items)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            slot.GetComponentInChildren<Image>().sprite = item.icon;
            Button equipButton = slot.GetComponentInChildren<Button>();
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() => EquipItem(item));
            Button dropButton = slot.transform.Find("DropButton").GetComponent<Button>();
            dropButton.onClick.RemoveAllListeners();
            dropButton.onClick.AddListener(() => DropItem(item));
        }
    }

    public void EquipItem(InventoryItem item)
    {
        if (_equippedWeapon != null)
        {
          _playerHealth.IncreaseDamage(-_equippedWeapon.damageAmount);
        }

        // Equip the new item
        _equippedWeapon = item;
        _playerHealth.IncreaseDamage(item.damageAmount);
        Debug.Log("Equipped: " + item.itemName);
        UpdateInventoryUI(); 
    }

    public void DropItem(InventoryItem item)
    {
        if (_equippedWeapon == item)
        {
            // Unequip current weapon effects before dropping
            _playerHealth.IncreaseDamage(-_equippedWeapon.damageAmount);
            _equippedWeapon = null;
        }
        RemoveItem(item);
       Debug.Log("Dropped: " + item.itemName);
      
    }

    public int GetEquippedWeaponDamage()
    {
        return _equippedWeapon != null ? _equippedWeapon.damageAmount : _playerHealth.BaseDamage;
    }
}

