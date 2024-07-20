using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Camera _camera;
    private PlayerMovementController _controller;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _controller = new(_playerMovement);
        _playerMovement.SetCamera(_camera);
    }

    // Update is called once per frame
    private void Update()
    {
        _controller.Update();
    }
}
