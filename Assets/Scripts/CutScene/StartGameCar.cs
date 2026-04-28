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
    public GameObject Canvas;
    public AudioSource carSound;
    public AudioSource carAudio;


    public bool exit = true;
    void Start()
    {
      carSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Invoke("Exit", 16f);

    }

    public void Exit()
    {
       carAudio.Stop();
        if (carCutScene.play)
        {
            if (exit)
            {
                Canvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    Canvas.SetActive(false);
                    Destroy(carCam);
                    carSound.Play();
                    OutsideCarCam.SetActive(true);
                    animator.SetTrigger("Exit");
                   
                    Invoke("EnablePlayer", 3f);
                    exit = false;
                }
            }
        }
      
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
