using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Camera _camera;
    private Coroutine _moveCoroutine;
    public float distanceToStop = 1.0f;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = 0.5f;
    }

    public void Move()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),
                out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;
            IInteractable currentTarget =
                clickedObject.GetComponent<IInteractable>();
            if (currentTarget != null)
            {
                // Move player to a position near the clicked object
                _agent.stoppingDistance = distanceToStop;
                Vector3 directionToTarget =
                    (hit.point - transform.position).normalized;
                Vector3 destination =
                    hit.point - directionToTarget * distanceToStop;
                _agent.SetDestination(destination);

                if (_moveCoroutine != null)
                {
                    StopCoroutine(_moveCoroutine); 
                }
                _moveCoroutine = StartCoroutine(CheckArrival(currentTarget));
            }
            else
            {
                _agent.stoppingDistance = 0.5f;
                _agent.SetDestination(hit.point);
            }
        }
    }

    public void SetCamera(Camera camera) =>
        _camera = camera;


    private IEnumerator CheckArrival(IInteractable target)
    {
        while (_agent.pathPending)
        {
            yield return null;
        }
        
        while (true)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance &&
                !_agent.pathPending)
            {
                target.OnInteract();
                yield break;
            }
            yield return null;
        }
    }
}

