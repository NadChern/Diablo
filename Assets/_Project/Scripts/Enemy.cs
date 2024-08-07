using UnityEngine;

namespace _Project
{
    public class Enemy : BotBehavior, IInteractable
    {
        [SerializeField] private int _experienceReward = 100;
        [SerializeField] private GameObject lootPrefab;
        [SerializeField] private Transform lootSpawnPoint; // point where the loot will be spawned
        [SerializeField] private Health _health;
        [SerializeField] private PlayerStats _playerStats;
        
        private LootService _lootService;

        public void Construct(LootService lootService)
        {
            _lootService = lootService;
        }
        
        protected override void Start()
        {
            base.Start();
            _health.OnDeath += DropLoot; 
            _health.OnDeath += OnEnemyDeath;
        }

        private void DropLoot()
        {
            _lootService.Drop(lootPrefab, lootSpawnPoint.position);
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
