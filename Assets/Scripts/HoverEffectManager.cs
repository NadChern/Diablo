using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffectManager : MonoBehaviour
{
    public GameObject hoverEffectPrefab; // Assign the HoverCircle prefab in the Inspector
    private GameObject currentEffect; // green circle
    private Transform lastHoveredObject; // track last hovered object

    // public float enemyOffset = 0.9f; // Offset for enemies (capsules)
    // public float lootOffset = 0.5f;  // Offset for loot (cubes)

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Transform hoveredTransform = hit.transform;
            IInteractable interactable = hoveredTransform.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (lastHoveredObject != hoveredTransform)
                {
                    ShowEffect(hoveredTransform);
                    lastHoveredObject = hoveredTransform;
                }
            }
            else
            {
                // If the object is not an interactable object, hide the effect
                HideEffect();
                lastHoveredObject = null;
            }
        }
        else
        {
            // If nothing is hit, hide the effect
            HideEffect();
            lastHoveredObject = null;
        }
    }
            
    //         
    //         
    //         if (hoveredTransform.CompareTag("Enemy") || hoveredTransform.CompareTag("Loot"))
    //         {
    //             if (lastHoveredObject != hoveredTransform)
    //             {
    //                 ShowEffect(hoveredTransform);
    //             }
    //             lastHoveredObject = hoveredTransform; // next frame i will be here if my mouse still will be on enemy or loot
    //             
    //             // If i remove lastHoveredObject
    //             // Every frame, the script would call ShowEffect, which would
    //             // destroy the current effect and instantiate a new one,
    //             // even if the mouse is still over the same object.
    //             // Continuously creating and destroying GameObjects every frame
    //             // is inefficient.  
    //             // The continuous creation and destruction of the hover effect
    //             // could cause flickering or other visual artifacts,
    //             // making the effect appear unstable and distracting to the player.
    //             
    //         }
    //         else
    //         {
    //             HideEffect();
    //         }
    //     }
    //     else
    //     {
    //         HideEffect();
    //     }
    // }

    void ShowEffect(Transform target)
    {
        if (currentEffect != null)
        {
            Destroy(currentEffect);
        }
        currentEffect = Instantiate(hoverEffectPrefab, target.position, Quaternion.identity, target);
        // // Set the offset based on the tag of the target
        // float offset = target.CompareTag("Enemy") ? enemyOffset : lootOffset;
        //
        // // Instantiate the effect at the target position with the appropriate offset
        // Vector3 targetPosition = target.position;
        // targetPosition.y -= offset; // Adjust the Y position with the offset
        //
        // currentEffect = Instantiate(hoverEffectPrefab, targetPosition, Quaternion.identity);
        // currentEffect.transform.SetParent(target);
        // currentEffect.transform.localPosition = new Vector3(0, -offset, 0); // Adjust the Y position relative to the parent
    }

    void HideEffect()
    {
        if (currentEffect != null)
        {
            Destroy(currentEffect);
        }
        lastHoveredObject = null;
    }
}
