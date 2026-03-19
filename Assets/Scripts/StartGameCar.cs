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
    public GameObject Trees;
    public GameObject Car;
    public GameObject Terrain;
    
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
        Destroy(carCam);
        OutsideCarCam.SetActive(true);
        animator.SetTrigger("Exit");
        Invoke("EnablePlayer", 3f);
    }

    public void EnablePlayer()
    {
        Destroy(car);
        Destroy(Trees);
        Destroy(Car);
        Destroy(OutsideCarCam);
        Destroy(Terrain);
        Destroy(Car);
       
    }
}
