using UnityEngine;
using _Project;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string itemName { get; private set; }
    public Sprite icon { get; private set; }
    public int healAmount { get; private set; }
    public int damageAmount { get; private set; }
}
