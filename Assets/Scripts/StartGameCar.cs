using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCar : MonoBehaviour
{
    public Animator animator;
    public GameObject carCam;
    public GameObject OutsideCarCam;
    public GameObject car;
    public CarCutScene carCutScene;
    public bool exit = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carCutScene.play)
        {
            if (exit)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Exit(); 
                    exit = false;
                }
            }
        }
    }

    public void Exit()
    {
        carCam.SetActive(false);
        OutsideCarCam.SetActive(true);
        animator.SetTrigger("Exit");
        Invoke("EnablePlayer", 1f);
    }

    public void EnablePlayer()
    {
        car.SetActive(false);
       OutsideCarCam.SetActive(false );
    }
}
