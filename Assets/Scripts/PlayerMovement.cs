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
    public float stoppingDistance = 1.0f;

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
            IInteractable interactable =
                clickedObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Move player to a position near the clicked object
                                _agent.stoppingDistance = stoppingDistance;
                                Vector3 directionToTarget =
                                    (hit.point - transform.position).normalized;
                                Vector3 destination =
                                    hit.point - directionToTarget * stoppingDistance;
                                _agent.SetDestination(destination);
                                
                                // Optionally, interact with the object immediately
                                interactable.OnInteract();
            }    
            else 
            {
                // Move player to the clicked point
                _agent.stoppingDistance =
                    0.5f; // Reset to default stopping distance
                _agent.SetDestination(hit.point);
            }
        }
    }

    public void SetCamera(Camera camera) =>
        _camera = camera;
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
        
        
    

