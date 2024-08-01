using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]  private PlayerInteraction _playerInteraction;
        private NavMeshAgent _agent;
        // private Camera _camera;
        private Coroutine _moveCoroutine;
  
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.stoppingDistance = 0.5f;
     }

        public void Move(Vector3 destination, IInteractable currentTarget)
        {
            _agent.SetDestination(destination);
            if (currentTarget != null)
            {
                StartArrivalCoroutine(currentTarget);
            }
        }

        // public void SetCamera(Camera cam) => _camera = cam;

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

       

