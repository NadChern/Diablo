
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController
{
    private readonly PlayerMovement _playerMovement;
    private readonly Camera _camera;
    private readonly NavMeshAgent _agent;

    public PlayerMovementController(PlayerMovement playerMovement, Camera camera)
       {
        _playerMovement = playerMovement;
        _camera = camera;
        _agent = playerMovement.GetComponent<NavMeshAgent>();
       }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),
                    out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                IInteractable currentTarget =
                    clickedObject.GetComponent<IInteractable>();
                
                Vector3 destination;
                if (currentTarget != null)
                {
                    _agent.stoppingDistance = _playerMovement.DistanceToStop;
                    Vector3 directionToTarget =
                        (hit.point - _playerMovement.transform.position)
                        .normalized;
                    destination = hit.point - directionToTarget *
                        _playerMovement.DistanceToStop;
                  
                }
                else
                {
                    _agent.stoppingDistance = 0.5f;
                    destination = hit.point;
                }
               
                _playerMovement.Move(destination, currentTarget);
            }
        }
    }
}
