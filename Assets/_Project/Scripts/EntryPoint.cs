using UnityEngine;

namespace _Project
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Camera _camera;
        [SerializeField] private Enemy[] _enemies;
        private PlayerMovementController _controller;
        

        private void Start()
        {
            _controller =
                new PlayerMovementController(_playerMovement, _camera);
            // _playerMovement.SetCamera(_camera);
            foreach (Enemy enemy in _enemies)
            {
                enemy.SetPlayer(_playerMovement.transform);
            }
        }

        private void Update()
        {
            _controller.Update();
        }
    }
}
