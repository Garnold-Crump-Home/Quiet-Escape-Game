using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Transform hand;
    public GameObject PrefabToThrow;
    public Transform objectToThrow;
    public Rigidbody rb;
    public float throwForce = 1f;
    public bool isThrown = false;
    public Animator animator;
    private bool dropobj = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (!isThrown)
        {
            if(Input.GetMouseButtonDown(0))
            {
               
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isThrown = true;
                animator.SetBool("isIdle", false);
                animator.SetTrigger("DropObj");
                dropobj = true;
                if (dropobj)
                {
                    Invoke("Throw", 1f);
                }
               
               
                
            }
        }
    }
    void Throw()
    {
        if (isThrown)
        {
            PrefabToThrow.SetActive(true);
            this.gameObject.SetActive(false);
            objectToThrow.transform.parent = hand;
            objectToThrow.position = new Vector3(hand.position.x, hand.position.y, hand.position.z);
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            objectToThrow.parent = null;
            isThrown = false;
            dropobj = false;
        }
    }
}
