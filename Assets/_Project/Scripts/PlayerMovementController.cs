using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace _Project
{
    public class PlayerMovementController
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Camera _camera;
        private readonly NavMeshAgent _agent;
        private readonly PlayerAttackSettings _attackSettings;

        public PlayerMovementController(PlayerMovement playerMovement,
            Camera camera, PlayerAttackSettings attackSettings)
        {
            _playerMovement = playerMovement;
            _camera = camera;
            _agent = playerMovement.GetComponent<NavMeshAgent>();
            _attackSettings = attackSettings;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),
                        out RaycastHit hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    Vector3 destination;

                    if (clickedObject.TryGetComponent(out IInteractable currentTarget))
                    {
                        _agent.stoppingDistance = _attackSettings.CurrentRange;
                        Vector3 directionToTarget = (hit.point - _playerMovement.transform.position).normalized;
                        destination = hit.point - directionToTarget * _attackSettings.CurrentRange;
                        
                        _playerMovement.Move(destination, currentTarget);
                    }
                    else
                    {
                        _agent.stoppingDistance = _attackSettings.DefaultStopDistance;
                        destination = hit.point;
                        
                        _playerMovement.Move(destination);
                    }
                }
            }
        }
    }
}
