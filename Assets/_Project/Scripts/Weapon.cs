using UnityEngine;

namespace _Project
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ItemsStorage/Weapon")]
    public class Weapon : Item
    {
        public float Damage;
        public float Range;
        public float Cooldown;
    }
}
