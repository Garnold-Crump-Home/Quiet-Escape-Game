using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraOn : MonoBehaviour
{
    public GameObject Cam;
    public SmartAvoidance SmartAvoidance;
    public SmartAvoidance SmartAvoidance2;
    public GameObject player;
    public GameObject PlayerCanvas;
    void Start()
    {
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
           PlayerActive();
        }
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
        PlayerCanvas.SetActive(true);
        Destroy(this.gameObject);
    }
}
