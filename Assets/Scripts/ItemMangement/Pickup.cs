using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways] // For scene view visualization
public class Pickup : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;
    public Transform Hand;
    public GameObject heldObject;
    public Animator animator;
    public PlayerMovement playerMovement;
    public GameObject canvas;

    [Header("Pickup Settings")]
    public float maxDistance = 4f;
    public float detectionRadius = 0.3f;

    void Update()
    {
        if (playerCamera == null) return;

        // DROP OBJECT
        if (playerMovement != null && playerMovement.HoldingObj && Input.GetKeyDown(KeyCode.Q))
        {
            DropObject();
            return;
        }

        
        if (playerMovement != null && !playerMovement.HoldingObj)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

          
            if (Physics.SphereCast(ray, detectionRadius, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
            {
                
                Pickup pickup = hit.collider.GetComponentInParent<Pickup>();
                if (pickup != null)
                {
                    canvas.SetActive(true);

                    if (Application.isPlaying && Input.GetKeyDown(KeyCode.E))
                    {
                        animator.SetTrigger("Pickup");
                        pickup.PickupObj();
                    }

                    return;
                }
            }

            
            canvas.SetActive(false);
        }
    }

    public void PickupObj()
    {
        if (!Application.isPlaying) return;

        canvas.SetActive(false);

        transform.SetParent(Hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        heldObject.SetActive(true);
        gameObject.SetActive(false);

        playerMovement.HoldingObj = true;
    }

    void DropObject()
    {
        
            playerMovement.HoldingObj = false;
        
    }

    // SCENE VIEW VISUALIZATION
    void OnDrawGizmos()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        bool hittingPickup = false;

        if (Physics.SphereCast(ray, detectionRadius, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.GetComponentInParent<Pickup>() != null)
            {
                hittingPickup = true;
            }
        }

        Gizmos.color = hittingPickup ? Color.red : Color.green;
        Vector3 origin = playerCamera.position;
        Vector3 end = origin + playerCamera.forward * maxDistance;

        Gizmos.DrawWireSphere(origin, detectionRadius);
        Gizmos.DrawWireSphere(end, detectionRadius);
        Gizmos.DrawLine(origin, end);
    }
}