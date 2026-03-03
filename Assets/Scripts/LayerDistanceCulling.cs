using UnityEngine;

public class LayerDistanceCulling : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
      

        // Create an array of 32 floats (one for each layer)
        float[] distances = new float[32];
        distances[0] = 100f; // Default layer
        distances[6] = 60f; 
        distances[8] = 50f;

        // Example: Layer 9 (Buildings) will stop rendering at 1000m
        distances[9] = 1000f;

        // Assign the distances to the camera
        cam.layerCullDistances = distances;
    }
}
