using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour
{
    public GameObject redKeyActivate;
    public bool redKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;
    public PlayerUITrigger playerUITrigger;
    void Start()
    {

    }

   
    void Update()
    {
        if (playerUITrigger.KeyCanUnlock)
        {
           KeyUnlock();
        }
        if(redKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }
    }

    public void KeyUnlock()
    {
        if (redKeyActivate.activeSelf)
        {



            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                redKeyUnlocked = true;
            }

        }

    }
}
