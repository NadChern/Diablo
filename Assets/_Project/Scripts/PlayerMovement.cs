using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private PlayerAttackSettings _attackSettings;
        private NavMeshAgent _agent;
        private Coroutine _moveCoroutine;
        private IInteractable _currentTarget;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _attackSettings.CurrentVelocity;
        }

        public void Move(Vector3 destination)
        {
            _playerInteraction.DropInteraction(); // drop current interaction
            _agent.speed = _attackSettings.CurrentVelocity;
            _agent.SetDestination(destination);
        }

        public void Move(Vector3 destination, IInteractable currentTarget)
        {
            // Check that you don't try to interact with same target
            // (to void auto-clicking for killing enemy faster)
            if (currentTarget != null && _currentTarget == currentTarget)
                return;
            
            _currentTarget = currentTarget;
            
            Move(destination);
            StartArrivalCoroutine(currentTarget);
        }

        private void StartArrivalCoroutine(IInteractable currentTarget)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _moveCoroutine = StartCoroutine(CheckArrival(currentTarget));
        }

        private IEnumerator CheckArrival(IInteractable target)
        {
            yield return new WaitUntil(() =>
                _agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending);
            _playerInteraction.Interact(target);
            _moveCoroutine = null;
        }
    }
}
