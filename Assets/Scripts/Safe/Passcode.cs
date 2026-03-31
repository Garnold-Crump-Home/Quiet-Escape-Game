using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passcode : MonoBehaviour
{
    private Transform player;
    public Transform playerCamera;
    public GameObject canvas;
    public Animator animator;
    public Safe safeScript;
    public Text answer1;
    public GameObject paper;
    public bool CanPickUp = false;





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            paper.SetActive(false);
        }
      
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

     

        if (Physics.Raycast(ray, out hit) && CanPickUp)
        {
            if (hit.transform == this.transform)
            {
                canvas.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {

                    animator.SetTrigger("Pickup");
                    Invoke("PickupObj", 1.2f);

                }


                return; 
            }
        }
    }
    public void PickupObj()
    {
paper.SetActive(true);
        answer1.text = safeScript.codeAnswer;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { CanPickUp = true; } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { CanPickUp = false; }
    }
}