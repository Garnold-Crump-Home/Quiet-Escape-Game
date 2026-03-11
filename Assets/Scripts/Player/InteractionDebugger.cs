using UnityEngine;

[ExecuteAlways]
public class InteractionDebugger : MonoBehaviour
{
    public Transform playerCamera;
    public float maxDistance = 6f;
    public float radius = 0.3f;

    void OnDrawGizmos()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        bool hittingPickup = false;

        if (Physics.SphereCast(ray, radius, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                hittingPickup = true;
            }
        }

        Gizmos.color = hittingPickup ? Color.red : Color.green;

        Vector3 origin = playerCamera.position;
        Vector3 end = origin + playerCamera.forward * maxDistance;

        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawWireSphere(end, radius);
        Gizmos.DrawLine(origin, end);
    }
}