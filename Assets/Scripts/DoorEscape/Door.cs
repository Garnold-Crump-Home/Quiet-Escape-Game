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
 
    public bool KeyCanUnlock;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (woodFall.woodIsFalling && redKey.redKeyUnlocked && blueKey.blueKeyUnlocked && greenKey.greenKeyUnlocked)
        {


         
            if (doorOpen == false)
            {
                if (KeyCanUnlock)
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
                if (KeyCanUnlock)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyCanUnlock = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            KeyCanUnlock = false;
        }
    }



}
