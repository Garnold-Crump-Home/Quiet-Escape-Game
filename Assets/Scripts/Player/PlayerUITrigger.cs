using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUITrigger : MonoBehaviour
{
    public GameObject playerUICanvas; 
    public bool KeyCanUnlock = false;
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
           
         
                playerUICanvas.SetActive(true); 
            KeyCanUnlock = true;
        }
    }

    private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
              
                    playerUICanvas.SetActive(false);
                KeyCanUnlock = false ;
            }
    }
}
