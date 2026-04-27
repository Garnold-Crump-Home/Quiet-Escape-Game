using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKey : MonoBehaviour
{

    public GameObject greenKeyActivate;
    public bool greenKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;
    public PlayerUITrigger playerUITrigger;
    public AudioSource keyUnlockSound;
    void Start()
    {
        keyUnlockSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerUITrigger.KeyCanUnlock) { KeyUnlock(); } 
       
        if (greenKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }

    }
    public void KeyUnlock()
    {
        if (greenKeyActivate.activeSelf)
        {




            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                greenKeyUnlocked = true;
                keyUnlockSound.Play();
            }


        }
    }
}
