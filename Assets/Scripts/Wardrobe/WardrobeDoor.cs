using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoor : MonoBehaviour
{
    public Transform player;
    public bool isOpen = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (!isOpen)
        {
            if (distance <= 6f && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("OpenDoor", true);
                GetComponent<Animator>().SetBool("CloseDoor", false);
                isOpen = true;
            }
        }
        else
        {
            if (distance <= 6f && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("OpenDoor", false);
                GetComponent<Animator>().SetBool("CloseDoor", true);

                isOpen =false;
            }
        }
    }
}
