using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;
    public float maxFps;
    public string Fps;
    public Text fpsText;
        public Slider fpsSlider;

    private Light mainLight;
    
    void Start()
    {
       
        GameObject lightObj = GameObject.FindWithTag("MainLight");
        if (lightObj != null)
            mainLight = lightObj.GetComponent<Light>();
        else
            Debug.LogWarning("MainLight not found!");

      
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        volumeSlider.value = AudioListener.volume;

        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

      
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        fpsSlider.onValueChanged.AddListener(SetFpsMax);
    }

    // QUALITY SETTINGS
    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);

        if (mainLight == null) return;

        switch (index)
        {
            case 0: // Low
                QualitySettings.shadows = ShadowQuality.Disable;
                mainLight.shadows = LightShadows.None;
                break;

            case 1: // Medium
                QualitySettings.shadows = ShadowQuality.HardOnly;
                mainLight.shadows = LightShadows.Hard;
                break;

            case 2: // High
                QualitySettings.shadows = ShadowQuality.All;
                mainLight.shadows = LightShadows.Soft;
                break;
        }
    }

    // RESOLUTION SETTINGS
    void SetResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
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

    
    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    void SetFpsMax(float value)
    {
        Application.targetFrameRate = (int)maxFps; 
        fpsSlider.maxValue = 300;
        fpsSlider.minValue = 30;
        
        fpsText.text = fpsSlider.value.ToString();
        
       
        
    }
}