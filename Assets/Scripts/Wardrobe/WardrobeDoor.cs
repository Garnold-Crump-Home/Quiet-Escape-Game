using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoor : MonoBehaviour
{
   
    public bool isOpen = false;
    public bool CanOpen =false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        if (!isOpen)
        {
            if (CanOpen && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("OpenDoor", true);
                GetComponent<Animator>().SetBool("CloseDoor", false);
                isOpen = true;
            }
        }
        else
        {
            if (CanOpen && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("OpenDoor", false);
                GetComponent<Animator>().SetBool("CloseDoor", true);

                isOpen =false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanOpen = true;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanOpen=false;
        }
    }
}
