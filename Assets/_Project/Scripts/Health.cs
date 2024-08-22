using System;
using UnityEngine;

namespace _Project
{
    public class Health : MonoBehaviour
    {
        [field:SerializeField] public float MaxHealth { get; private set; } = 20;
        [field:SerializeField] public float CurrentHealth { get; private set; }
        public event Action OnHealthChanged;
        public event Action OnDeath;

        
        private void Start()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        { 
            Debug.Log("Damage received: " + damage);
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

        public void SetMaxHealth(float newMaxHealth)
        {
            MaxHealth = newMaxHealth;
            CurrentHealth = MaxHealth; // heal to max health on level up
            OnHealthChanged?.Invoke();
        }
        private void HandleDeath()
        {
            Destroy(gameObject);
        }
    }
}
