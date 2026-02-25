using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    public GameObject obj8;
    public GameObject door;
    public WardrobeCollider wardrobeCollider;
    public PlayerMovement playerMovement;
    private bool obj1Completed = false;
    private bool obj2Completed = false;
    private bool fCompleted = false;
    private bool obj3Completed = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!obj1Completed) {
            if (playerMovement.HoldingObj)
            {

                obj1.SetActive(false);

                if (Input.GetMouseButtonDown(0))
                {
                    obj2.SetActive(false);
                    fCompleted = true;


                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (fCompleted)
                    {


                        obj1Completed = true;
                        obj3.SetActive(false);
                        obj1Completed = true;
                        obj4.SetActive(true);
                        obj5.SetActive(true);
                        obj6.SetActive(true);
                    }
                }
            }
        }

        if (obj1Completed)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                obj4.SetActive(false);
            }
             if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                obj5.SetActive(false);
              
            }
             if(Input.GetKeyDown(KeyCode.Space))
            {
                obj6.SetActive(false);
                obj2Completed = true;
               
            }
        }
        if (obj2Completed)
        {
           

            obj7.SetActive(true);

            if (wardrobeCollider.isInWardrobe)
            {
                obj7.SetActive(false);
                if (obj3Completed == false)
                {
                
                    obj8.SetActive(true);
                    obj3Completed = true;
                }

            }

            if(Input.anyKeyDown)
            {
                obj8.SetActive(false);
                door.SetActive(true);
            }
        }
    }
}
