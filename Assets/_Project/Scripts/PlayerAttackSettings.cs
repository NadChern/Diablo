using UnityEngine;

namespace _Project
{
    public class PlayerAttackSettings : MonoBehaviour
    {
        [SerializeField] private float defaultDamage = 1.0f;
        [SerializeField] private float defaultRange = 1.5f;
        [SerializeField] private float defaultCooldown = 1.0f;
        [SerializeField] private float defaultVelocity = 4.5f;
        [SerializeField] private float defaultDamageResistance = 0.1f;
        [SerializeField] private float defaultStopDistance = 1.0f;
        public float DefaultStopDistance => defaultStopDistance;

        public float CurrentDamage { get; private set; }
        public float CurrentRange { get; private set; }
        public float CurrentCooldown { get; private set; }
        public float CurrentVelocity { get; private set; }
        public float CurrentDamageResistance { get; private set; }


        private void Start()
        {
            ResetToDefault();
        }

        public void UpdateWeapon(float damage, float range, float cooldown)
        {
            CurrentDamage = damage;
            CurrentRange = range;
            CurrentCooldown = cooldown;
        }

        public void UpdateGear(float velocity, float damageResistance)
        {
            CurrentVelocity = velocity;
            CurrentDamageResistance = damageResistance;
        }

        public void ResetWeapon()
        {
            CurrentDamage = defaultDamage;
            CurrentRange = defaultRange;
            CurrentCooldown = defaultCooldown;
        }

        public void ResetGear()
        {
            CurrentVelocity = defaultVelocity;
            CurrentDamageResistance = defaultDamageResistance;
        }

        public void ResetToDefault()
        {
            ResetWeapon();
            ResetGear();
        }
    }
}
