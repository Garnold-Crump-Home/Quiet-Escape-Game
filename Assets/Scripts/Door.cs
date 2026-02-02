using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    public Animator DoorAnimation;
    public bool doorOpen = false;


    public float interactionDistance = 5f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(player.position, transform.position);
        if (doorOpen == false)
        {
            if (distance <= interactionDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoorAnimation.SetBool("Closed", false);
                    DoorAnimation.SetBool("DoorOpen", true);
                    
                    doorOpen = true;
                }
            }
        }
        else if (doorOpen == true)
        {
            if (distance <= interactionDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                 doorOpen =false;
                    DoorAnimation.SetBool("DoorOpen", false);
                    DoorAnimation.SetBool("Closed", true);
                    

                }
            }
        }
    }

    
}
