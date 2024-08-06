using UnityEngine;

namespace _Project
{
    public class Enemy : BotBehavior, IInteractable
    {
        [SerializeField] private int _experienceReward = 100;
        [SerializeField] private GameObject lootPrefab;
        [SerializeField] private Transform lootSpawnPoint; // Point where the loot will be spawned
        [SerializeField] private Health _health;
        [SerializeField] private PlayerStats _playerStats;

        protected override void Start()
        {
            base.Start();
            _health.OnDeath += DropLoot; // Subscribe to the OnDeath event
            _health.OnDeath += OnEnemyDeath;
        }

        private void DropLoot()
        {
            if (lootPrefab != null && lootSpawnPoint != null)
            {
                Instantiate(lootPrefab, lootSpawnPoint.position, Quaternion.identity);
                Debug.Log("Loot dropped.");
            }
        }

        private void OnEnemyDeath()
        {
            if (_playerStats != null)
            {
                _playerStats.GainExperience(_experienceReward);
            }
        }
    }
}
