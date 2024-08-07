using Unity.AI.Navigation;
using UnityEngine;

namespace _Project
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Camera _camera;
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private PlayerAttackSettings _attackSettings;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        
        private PlayerMovementController _controller;
        private LootService _lootService;

        private void Start()
        {
            _lootService = new(_navMeshSurface);
            _controller = new PlayerMovementController(_playerMovement, _camera, _attackSettings);

            foreach (Enemy enemy in _enemies)
            {
                enemy.SetPlayer(_playerMovement.transform);
                enemy.Construct(_lootService);
            }
        }

        private void Update()
        {
            _controller.Update();
        }
    }
}
