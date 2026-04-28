using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoor : MonoBehaviour
{
    public Transform player;
    public Animator DoorAnimation;
    public bool doorOpen = false;
    public bool CanOpenDoor;
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;






    void Start()
    {
        doorOpenSound = GetComponent<AudioSource>();
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
                    doorOpenSound.Play();
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
                    doorCloseSound.Play();
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




