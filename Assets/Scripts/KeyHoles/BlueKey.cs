using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKey : MonoBehaviour
{
   
    public GameObject blueKeyActivate;
    public bool blueKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;
    public PlayerUITrigger playerUITrigger;
    public AudioSource keyUnlockSound;

    void Start()
    {
        keyUnlockSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUITrigger.KeyCanUnlock) {
        KeyUnlock();
        }
    
        if (blueKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }
    }

    public void KeyUnlock()
    {
        if (blueKeyActivate.activeSelf)
        {

            

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                blueKeyUnlocked = true;
                keyUnlockSound.Play();
            }

        }
    }
}
