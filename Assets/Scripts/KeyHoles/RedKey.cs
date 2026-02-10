using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour
{
    public Transform redKey;
    public GameObject redKeyActivate;
    public bool redKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(redKey.position, transform.position);
        if (redKeyActivate.activeSelf)
        {


            if (distance <= 3f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    redKeyUnlocked = true;
                }
            }
        }
        if(redKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }
    }
}
