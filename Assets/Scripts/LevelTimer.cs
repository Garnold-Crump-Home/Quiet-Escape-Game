using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text text;
    public float time = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        text.text = time.ToString(); UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);

        // Format the text to display leading zeros (e.g., 01:05)
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

