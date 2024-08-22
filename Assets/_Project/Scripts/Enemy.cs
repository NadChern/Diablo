using UnityEngine;

namespace _Project
{
    public class Enemy : BotBehavior, IInteractable
    {
        [SerializeField] private int _experienceReward = 50;
        [SerializeField] private Transform lootSpawnPoint;
        [SerializeField] private Health _health;
        [SerializeField] private PlayerStats _playerStats;

        private string[] _lootIds;
        private LootService _lootService;

        public void Construct(LootService lootService, ItemsStorage itemsStorage)
        {
            _lootService = lootService;
            _lootIds = itemsStorage.GetAllItemIds();
        }

        protected override void Start()
        {
            base.Start();
            _health.OnDeath += DropLoot;
            _health.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy()
        {
            _health.OnDeath -= DropLoot;
            _health.OnDeath -= OnEnemyDeath;
        }

        private void DropLoot()
        {
            if (_lootService == null)
            {
                Debug.LogError("LootService is not set.");
                return;
            }

            if (_lootIds == null || _lootIds.Length == 0)
            {
                Debug.LogError("No loot IDs available.");
                return;
            }
            
            string randomLootId = _lootIds[Random.Range(0, _lootIds.Length)];
            Debug.Log($"Dropping loot with ID {randomLootId} at {lootSpawnPoint.position}");
            _lootService.Drop(randomLootId, lootSpawnPoint.position);
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
