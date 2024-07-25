using UnityEngine;


    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Camera _camera;
        private PlayerMovementController _controller;

        private void Start()
        {
            _controller =
                new PlayerMovementController(_playerMovement, _camera);
            _playerMovement.SetCamera(_camera);
        }

        private void Update()
        {
            _controller.Update();
        }
    }

