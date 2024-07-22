using System;
using System.Collections;
using System.Collections.Generic;
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
        // Wait until the path is fully calculated and the agent starts moving
        while (_agent.pathPending)
        {
            yield return null;
        }

        // Check if the agent has reached the target
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

// if (Input.GetMouseButtonDown(0))
// {
//     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//     Debug.DrawRay(ray.origin, ray.direction * 100, Color.red,
//         2.0f); // Draw the ray for 2 seconds
//     RaycastHit hit;
//     if (Physics.Raycast(ray, out hit))
//     {
//         GameObject clickedObject = hit.collider.gameObject;
//         Debug.Log("Clicked object tag: " + clickedObject.tag);
//         if (clickedObject.CompareTag("Enemy") ||
//             clickedObject.CompareTag("Loot"))
//         {
//             // Move player to a position near the clicked object
//             agent.stoppingDistance = stoppingDistance;
//             Vector3 directionToTarget =
//                 (hit.point - transform.position).normalized;
//             Vector3 destination =
//                 hit.point - directionToTarget * stoppingDistance;
//             agent.SetDestination(destination);
//             Debug.Log("Moving to " + clickedObject.tag +
//                       " at distance: " + stoppingDistance);
//         }
//         else
//         {
//             // Move player to the clicked point
//             agent.stoppingDistance =
//                 0.5f; // Reset to default stopping distance
//             agent.SetDestination(hit.point);
//             Debug.Log(
//                 "Moving to point with default stopping distance.");
//         }
//     }
// }
