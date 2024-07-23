using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Camera _camera;
    private PlayerMovementController _controller;
    
    void Start()
    {
        _controller = new(_playerMovement);
        _playerMovement.SetCamera(_camera);
    }
    
    void Update()
    {
        _controller.Update();
    }
}
