using UnityEngine;

public class PlayerMovementController
{
    private readonly PlayerMovement _playerMovement;

    public PlayerMovementController(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerMovement.Move();
        }
    }
}
