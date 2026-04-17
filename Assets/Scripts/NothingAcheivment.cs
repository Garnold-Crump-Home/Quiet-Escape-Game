using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingAcheivment : MonoBehaviour
{
    public LevelTimer levelTimer;
    public GameObject  achievementUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
           if(levelTimer.time < 60f)
            {
               Destroy(gameObject);
            }
        }

        if (levelTimer.time >= 60f && levelTimer.time < 61f) // 1 minute in seconds
        {
            UnlockAcheivment();
        }
    }

    void UnlockAcheivment()
    {
       achievementUI.SetActive(true);
        Invoke("uiOff", 5f); // Hide the UI after 5 seconds
    }

    void uiOff()
    {
        achievementUI.SetActive(false);
    }
}
