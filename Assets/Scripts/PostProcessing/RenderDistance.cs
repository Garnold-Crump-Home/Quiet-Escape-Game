using UnityEngine;

public class RenderDistance : MonoBehaviour
{
    // Assign your main camera here in the Inspector
    public Camera mainCamera;

    // Set the desired render distance (e.g., 500 meters)
    public float renderDistance = 500f;

    void Start()
    {
        // Ensure the camera reference is set, use Camera.main if not specified
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            // Set the far clipping plane distance
            mainCamera.farClipPlane = renderDistance;
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }
}
