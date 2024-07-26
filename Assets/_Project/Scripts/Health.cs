using System;
using UnityEngine;

namespace _Project
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; private set; } = 10;
        public float CurrentHealth { get; private set; }
        public event Action OnHealthChanged;
        public event Action OnDeath;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            if (CurrentHealth == 0)
            {
                OnDeath?.Invoke();
            }

            OnHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
            OnHealthChanged?.Invoke();
        }
    }
}
