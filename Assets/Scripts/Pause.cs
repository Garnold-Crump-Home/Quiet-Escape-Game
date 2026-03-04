using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
   public GameObject pauseMenu;
    public GameObject graphics;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                pauseMenu.SetActive(false);
                graphics.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
               
            }
            else
            {
                pauseMenu.SetActive(true);
              
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
