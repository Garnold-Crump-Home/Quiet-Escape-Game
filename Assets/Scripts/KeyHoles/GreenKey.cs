using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKey : MonoBehaviour
{
    public Transform greenKey;
    public GameObject greenKeyActivate;
    public bool greenKeyUnlocked = false;
    public Rigidbody rb;
    public Rigidbody rb2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(greenKey.position, transform.position);
        if (greenKeyActivate.activeSelf)
        {


            if (distance <= 3f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    greenKeyUnlocked = true;
                }
            }

        }
        if (greenKeyUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }

    }
}
