using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
                        Cursor.lockState = CursorLockMode.None;
        }
    }
}
