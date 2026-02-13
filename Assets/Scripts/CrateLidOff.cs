using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateLidOff : MonoBehaviour
{
    public Rigidbody[] nailsRb;
    
    

    void Start()
    {
       
    }

    void Update()
    {
        bool allNailsUnfrozen = true;

        foreach (Rigidbody nail in nailsRb)
        {
            if (nail.freezeRotation)
            {
                allNailsUnfrozen = false;
                break; // no need to keep checking
            }
        }

        if (allNailsUnfrozen)
        {
           
            Destroy(this.gameObject);
        }
    }
}


