using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSmallerNails : MonoBehaviour
{
    public bool holderingCrowbar = true;
    public Rigidbody nails;
    public Transform crowbar;
    public Animator animator;
    public bool hit;
    public bool canRemoveNails;
    public GameObject crowbarHolding;
    public Transform playerCamera;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (crowbarHolding.activeInHierarchy == false)
        {
            holderingCrowbar = false;
        }
        if (crowbarHolding.activeInHierarchy == true)
        {
            holderingCrowbar = true;
        }






        if (holderingCrowbar)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Hit");
                if (canRemoveNails)
                {
                    nails.freezeRotation = false;
                    nails.constraints = RigidbodyConstraints.None;
                    Debug.Log("Hit");


                }


            }
        }}
            




          
            
        



       

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canRemoveNails = true;
        }
    }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canRemoveNails = false;
            }
    }
}
