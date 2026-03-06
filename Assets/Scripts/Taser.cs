using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Taser : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float Ammo = 4f;
    public float reloadTime = 2f;
    public float launchVelocity = 700f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reloadTime -= Time.deltaTime;
        if (Ammo > 0)
        {
            if (reloadTime <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    reloadTime = 2f;
                    GameObject newProjectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    Ammo -= 1f;
                    Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(firePoint.forward * launchVelocity);

                    }


                    Destroy(newProjectile, 5f);
                }
            }
        }
    }
    }

