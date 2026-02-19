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





    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paper.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }
    public void PickupObj()
    {
paper.SetActive(true);
        answer1.text = safeScript.codeAnswer;



    }
}