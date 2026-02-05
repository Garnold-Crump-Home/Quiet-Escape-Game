using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public Transform Hand;
    public GameObject heldObject;
    public bool holdingObject = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= 4f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                  
                    this.transform.parent = playerCamera;
                    this.transform.position = new Vector3(Hand.position.x, Hand.position.y, Hand.position.z);
                 
                    heldObject.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);


                }
            }

        }
    
}
