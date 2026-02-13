using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Transform player;
    public FlashLightBattery flashlightBattery;
    public Animator pickup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= 4.5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                pickup.SetTrigger("Pickup");
                flashlightBattery.BatteryLevel += 50f;
                Destroy(gameObject);
            }
        }
        if( flashlightBattery.BatteryLevel > 100f)
        {
            flashlightBattery.BatteryLevel = 100f;
        }
    }
}
