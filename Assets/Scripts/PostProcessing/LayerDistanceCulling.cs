using UnityEngine;

public class LayerDistanceCulling : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
      

      
        float[] distances = new float[32];
        distances[0] = 80f;
        distances[6] = 75f; 
        distances[6] = 60f; 
        distances[8] = 50f;
        distances[9] = 70f;

       

      
        cam.layerCullDistances = distances;
    }
}
