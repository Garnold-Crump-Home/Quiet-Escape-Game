using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKey : MonoBehaviour
{
    public Transform blueKey;
    public GameObject blueKeyActivate;
    public bool blueKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(blueKey.position, transform.position);
        if (blueKeyActivate.activeSelf)
        {


            if (distance <= 3f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    blueKeyUnlocked = true;
                }
            }
        }
        if (blueKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }
    }
}
