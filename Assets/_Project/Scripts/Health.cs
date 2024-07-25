using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{   private int _currentHealth;
    public int maxHealth = 10;
    public UnityEvent onHealthChanged;
    public UnityEvent onDeath;
    
    void Start()
    {
        _currentHealth = maxHealth;
        // onHealthChanged.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onDeath.Invoke();
        }
        onHealthChanged.Invoke();
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        onHealthChanged.Invoke();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}

