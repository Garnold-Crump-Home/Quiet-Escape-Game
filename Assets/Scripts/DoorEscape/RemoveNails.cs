using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNails : MonoBehaviour
{
    public bool holderingCrowbar = true;
    public Rigidbody nails;
    public Transform crowbar;
    public Animator animator;
    public bool hit;
    public GameObject crowbarHolding;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(crowbarHolding.activeInHierarchy == false)
        {
            holderingCrowbar = false;
        }
        if(crowbarHolding.activeInHierarchy == true)
        {
            holderingCrowbar = true;
        }
       

            float distance = Vector3.Distance(crowbar.position, transform.position);
        if (holderingCrowbar)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Hit");
                if (distance <= 4f)
                {
                    nails.freezeRotation = false;
                    nails.constraints = RigidbodyConstraints.None;

                    
                }


            }
        }
    }
}
