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
    public Button pickupButton;
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
        if (!playerMovement.HoldingObj)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance >= 4.5f)
            { canvas.SetActive(false); }


            else if (distance <= 4f)
            {
                canvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetTrigger("Pickup");
                    Invoke("PickupObj", 1f);
                    canvas.SetActive(false);

                }
            }

        }
    }

    public void PickupObj()
    {
        this.transform.parent = playerCamera;
        this.transform.position = new Vector3(Hand.position.x, Hand.position.y, Hand.position.z);

        heldObject.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        playerMovement.HoldingObj = true;
    }
    
}
