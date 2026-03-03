using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Transform player;
    public FlashLightBattery flashlightBattery;
    public Animator animator;
    public GameObject canvas;
    public Transform playerCamera;

    public float maxDistance = 4f;
    public float detectionRadius = 0.3f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, detectionRadius, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
        {
            // Look for Battery script on parent (so child trigger works)
            Battery battery = hit.collider.GetComponentInParent<Battery>();

            if (battery != null)
            {
                canvas.SetActive(true);

                if (Application.isPlaying && Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetTrigger("Pickup");
                    battery.PickupObj();
                }

                return;
            }
        }

        canvas.SetActive(false);
    }
    public void PickupObj()
    {
        if (!Application.isPlaying) return;

        flashlightBattery.BatteryLevel += 50f;
                    Destroy(gameObject);


        if (flashlightBattery.BatteryLevel > 100f)
        {
            flashlightBattery.BatteryLevel = 100f;

        }

        }

    void OnDrawGizmos()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        bool hittingPickup = false;

        if (Physics.SphereCast(ray, detectionRadius, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.GetComponentInParent<Battery>() != null)
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



