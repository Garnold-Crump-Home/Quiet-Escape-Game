using UnityEngine;

public class LayerDistanceCulling : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
      

        // Create an array of 32 floats (one for each layer)
        float[] distances = new float[32];
        distances[0] = 80f; // Default layer
        distances[6] = 75f; 
        distances[6] = 60f; 
        distances[8] = 50f;
        distances[9] = 70f;

       

        // Assign the distances to the camera
        cam.layerCullDistances = distances;
    }
}
