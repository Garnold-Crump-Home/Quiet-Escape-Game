using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Transform player;
    public FlashLightBattery flashlightBattery;
    public Animator pickup;
    public GameObject canvas;
    public Transform playerCamera;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        float maxDistance = 4.5f;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.transform == this.transform)
            {
                canvas.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickup.SetTrigger("Pickup");
                    flashlightBattery.BatteryLevel += 50f;
                    Destroy(gameObject);
                }
            }
            if (flashlightBattery.BatteryLevel > 100f)
            {
                flashlightBattery.BatteryLevel = 100f;

            }

        }
    }
}

