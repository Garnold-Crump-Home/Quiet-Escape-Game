using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeCollider : MonoBehaviour
{
 public bool isInWardrobe = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInWardrobe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInWardrobe = false;
        }
    }
}
