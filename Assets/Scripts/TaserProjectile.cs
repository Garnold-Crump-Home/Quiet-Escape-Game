using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserProjectile : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnCollisionEnter(Collision collision)
    {

        if(CompareTag("Enemy"))
        {
            GameObject enemyObject = GameObject.FindWithTag("Enemy");
            rb = enemyObject.GetComponent<Rigidbody>();


            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

            Invoke("Unfreeze", 5f);
        }
        Invoke("DestroyProjectile", 2f);
    }

    void Unfreeze()
    {
              
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.None;
    }

    void DestroyProjectile()
    {
       Destroy(gameObject);
    }
}
