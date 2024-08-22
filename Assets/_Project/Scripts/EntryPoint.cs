using TriInspector;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerAttackSettings _attackSettings;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        [SerializeField] private ItemsStorage _itemsStorage;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private Health _playerHealth;
        [SerializeField] private PlayerStats _playerStats; 
        [SerializeField, ReadOnly] private Enemy[] _enemies;

        private PlayerMovementController _controller;
        private LootService _lootService;

        private void Start()
        {
            _lootService = new(_navMeshSurface, _itemsStorage);
            _controller = new PlayerMovementController(_playerMovement, _camera, _attackSettings);

            foreach (Enemy enemy in _enemies)
            {
                enemy.SetPlayer(_playerMovement.transform);
                enemy.Construct(_lootService, _itemsStorage);
            }

            _playerHealth.OnDeath += OnGameOver;
            _playerStats.OnLevelCompleted += OnGameOver;
        }

        private void Update()
        {
            _controller.Update();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnGameOver()
        {
            _gameOverScreen.SetActive(true);
        }

        private void OnDestroy()
        {
            _playerHealth.OnDeath -= OnGameOver;
            _playerStats.OnLevelCompleted -= OnGameOver;
        }


        [Button]
        private void CollectEnemies() =>
            _enemies = FindObjectsOfType<Enemy>();
    }
}
