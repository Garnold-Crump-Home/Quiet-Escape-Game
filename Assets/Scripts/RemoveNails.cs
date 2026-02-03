using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNails : MonoBehaviour
{
    public bool holderingCrowbar = true;
    public Rigidbody nails;
    public Transform crowbar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(crowbar.position, transform.position);
        if (holderingCrowbar)
        {
            if (distance <= 4f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                   
                    nails.freezeRotation = false;
                    nails.constraints = RigidbodyConstraints.None;
                }
            }
           

        }
    }
}
