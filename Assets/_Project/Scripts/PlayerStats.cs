using System;
using UnityEngine;

namespace _Project
{
    public class PlayerStats: MonoBehaviour
    {
        [SerializeField] private Health _health;
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; } = 0;
        public int LevelUpBase { get; private set; } = 300;
        public event Action OnStatsChanged;
        public event Action OnLevelCompleted;

        // Experience required for the next level
        public int ExperienceToNextLevel => LevelUpBase * Level;

        public void GainExperience(int amount)
        {
            Experience += amount;
            Debug.Log($"Gained {amount} experience. Total: {Experience}");

            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
            OnStatsChanged?.Invoke();  // Notify UI to update
        }

        private void LevelUp()
        {
            Level++;
            float newMaxHealth = _health.MaxHealth * 1.15f;
            _health.SetMaxHealth(newMaxHealth);
            Debug.Log($"Leveled up to {Level}! Health: {_health.MaxHealth}");
            OnStatsChanged?.Invoke();
            if (Level == 3)
            {
                OnLevelCompleted?.Invoke();
            }
        }
    }
}
