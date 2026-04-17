using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartUp : MonoBehaviour
{
   private bool isActivated = false;
    public SmartAvoidance smartAvoidance;
    public SmartAvoidance smartAvoidance2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
           isActivated = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           if(isActivated)
           {
              
               smartAvoidance.isActive = true;
                smartAvoidance2.isActive = true;
                Destroy(this);
            }
        }
    }
}
