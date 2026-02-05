using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    public Animator DoorAnimation;
    public bool doorOpen = false;
    public WoodFall woodFall;


    public float interactionDistance = 5f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (woodFall.woodIsFalling)
        {


            float distance = Vector3.Distance(player.position, transform.position);
            if (doorOpen == false)
            {
                if (distance <= interactionDistance)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        DoorAnimation.SetBool("Closed", false);
                        DoorAnimation.SetBool("Closed1", false);
                        DoorAnimation.SetBool("DoorOpen", true);
                        DoorAnimation.SetBool("DoorOpen1", true);

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

                        doorOpen = false;
                        DoorAnimation.SetBool("DoorOpen", false);
                        DoorAnimation.SetBool("DoorOpen1", false);
                        DoorAnimation.SetBool("Closed", true);
                        DoorAnimation.SetBool("Closed1", true);


                    }
                }
            }
        }
    }

    
}
