using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoor : MonoBehaviour
{
    public Transform player;
    public Animator DoorAnimation;
    public bool doorOpen = false;
    public bool CanOpenDoor;





    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        
            if (doorOpen == false)
            {
                if (CanOpenDoor)
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
                if (CanOpenDoor)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        doorOpen = false;
                        DoorAnimation.SetBool("DoorOpen", false);
                       
                        DoorAnimation.SetBool("Closed", true);
                     


                    }
                }
            }
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            CanOpenDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            CanOpenDoor = false;
        }
    }
}




