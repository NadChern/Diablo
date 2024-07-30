using System;
using UnityEngine;

namespace _Project
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; private set; } = 10;
        public float CurrentHealth { get; private set; }
        public int BaseDamage { get; private set; } = 1;
        public int CurrentDamage { get; private set; } // for weapon effect
        public event Action OnHealthChanged;
        public event Action OnDeath;

        
        private void Start()
        {
            CurrentHealth = MaxHealth;
            CurrentDamage = BaseDamage;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            if (CurrentHealth == 0)
            {
                OnDeath?.Invoke();
                HandleDeath();
            }

            OnHealthChanged?.Invoke();
        }

        public void IncreaseDamage(int amount)
        {
            CurrentDamage += amount;
        }
        
        public void Heal(int amount)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
            OnHealthChanged?.Invoke();
        }

        private void HandleDeath()
        {
            Destroy(gameObject);
        }
    }
}
