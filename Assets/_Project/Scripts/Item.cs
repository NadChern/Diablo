using UnityEngine;

namespace _Project
{
    public abstract class Item : ScriptableObject
    {
        public string Id;
        public ItemType ItemType;
        public Sprite Sprite;
    }

    public enum ItemType
    {
        Weapon,
        Potion,
        Armor
    }
}
