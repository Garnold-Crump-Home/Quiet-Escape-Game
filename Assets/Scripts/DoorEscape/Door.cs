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
    public RedKey redKey;
    public GreenKey greenKey;
    public BlueKey blueKey;
    public bool CanOpenDoor;
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;



    void Start()
    {
       doorOpenSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (woodFall.woodIsFalling && redKey.redKeyUnlocked && blueKey.blueKeyUnlocked && greenKey.greenKeyUnlocked)
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
        } }

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




