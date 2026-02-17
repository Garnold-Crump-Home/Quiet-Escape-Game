using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public Transform Hand;
    public GameObject heldObject;
    public bool holdingObject = false;
    public Animator animator;
    public PlayerMovement playerMovement;
    public GameObject canvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.HoldingObj && Input.GetKeyDown(KeyCode.Q))
        {
            playerMovement.HoldingObj = false;
        }
        else if (!playerMovement.HoldingObj)
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
                        
                        animator.SetTrigger("Pickup");
                        Invoke("PickupObj", 1.2f);
                        
                    }

                    return; // stop here so canvas doesn't get disabled
                }
            }

            canvas.SetActive(false);
        }
    }


    public void PickupObj()
    {
        canvas.SetActive(false);
        this.transform.parent = playerCamera;
        this.transform.position = new Vector3(Hand.position.x, Hand.position.y, Hand.position.z);

        heldObject.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        playerMovement.HoldingObj = true;
    }
    
}
