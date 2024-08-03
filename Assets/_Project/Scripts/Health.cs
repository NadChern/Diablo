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
            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            if (CurrentHealth == 0)
            {
                OnDeath?.Invoke();
                HandleDeath();
            }

            OnHealthChanged?.Invoke();
        }

        public void Heal(float amount)
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
