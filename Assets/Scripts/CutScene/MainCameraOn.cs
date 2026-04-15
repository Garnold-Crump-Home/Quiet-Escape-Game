using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraOn : MonoBehaviour
{
    public GameObject Cam;
    public SmartAvoidance SmartAvoidance;
    public SmartAvoidance SmartAvoidance2;
    public GameObject player;
    void Start()
    {
        
    }

   
    void Update()
    {
        Invoke("PlayerActive", 17f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Cam.SetActive(true);
            SmartAvoidance.enabled = true;
            SmartAvoidance2.enabled = true;
          
        }
    }

    void PlayerActive()
    {
        player.SetActive(true);
        Destroy(this.gameObject);
    }
}
