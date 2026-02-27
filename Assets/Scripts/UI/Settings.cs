using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown shadowsQuality;
 
    void Start()
    {
        
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        shadowsQuality.value = QualitySettings.shadows == ShadowQuality.Disable ? 0 : (QualitySettings.shadows == ShadowQuality.HardOnly ? 1 : 2);
    }

    // Update is called once per frame
    void Update()
    {
            Light light = GameObject.FindWithTag("MainLight").GetComponent<Light>();
        if (light != null)
        {
            if (QualitySettings.shadows != ShadowQuality.Disable)
            {
                light.shadows = LightShadows.None;
            }
            else
            {
                light.shadows = LightShadows.Hard;
            }
        } else { Debug.LogWarning("MainLight not found!"); }

        QualitySettings.SetQualityLevel(qualityDropdown.value);
        switch (shadowsQuality.value)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                break;
        }
        if (QualitySettings.shadows != ShadowQuality.Disable)
        {
            light.shadows = LightShadows.None;
        }
        else
        {
            light.shadows = LightShadows.Hard;
        }

     switch(resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1280, 720,  Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
              
                break;
        }
       

    }
}
