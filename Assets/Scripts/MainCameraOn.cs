using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraOn : MonoBehaviour
{
    public GameObject Cam;
    public SmartAvoidance SmartAvoidance;
    public SmartAvoidance SmartAvoidance2;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Cam.SetActive(true);
            SmartAvoidance.start = true;
            SmartAvoidance2.start = true;
        }
    }
}
