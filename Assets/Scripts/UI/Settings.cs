using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [Header("UI")]
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Slider fpsSlider;
    public Text fpsText;

    [Header("Scene")]
    public Light mainLight; // assign manually OR auto-find

    private Resolution[] resolutions;

    void Start()
    {
        // ✅ FPS setup
        QualitySettings.vSyncCount = 0;

        // ✅ Get resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        // ✅ Load saved settings
        int savedQuality = PlayerPrefs.GetInt("quality", QualitySettings.GetQualityLevel());
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        int savedFPS = PlayerPrefs.GetInt("fps", 60);
        int savedRes = PlayerPrefs.GetInt("resolution", currentResIndex);

        // ✅ Apply saved values
        qualityDropdown.value = savedQuality;
        volumeSlider.value = savedVolume;
        fpsSlider.value = savedFPS;
        resolutionDropdown.value = savedRes;

        // ✅ Apply immediately
        SetQuality(savedQuality);
        SetVolume(savedVolume);
        SetFpsMax(savedFPS);
        SetResolution(savedRes);

        // ✅ UI setup
        fpsSlider.minValue = 30;
        fpsSlider.maxValue = 300;
        fpsText.text = savedFPS.ToString();

        // ✅ Find light if not assigned
       

        // ✅ Listeners
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        fpsSlider.onValueChanged.AddListener(SetFpsMax);
    }

    private void Update()
    {
        if (mainLight == null)
        {
            GameObject lightObj = GameObject.FindWithTag("MainLight");
            if (lightObj != null)
                mainLight = lightObj.GetComponent<Light>();
        }
    }
    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);

        PlayerPrefs.SetInt("quality", index);

        if (mainLight == null)
        {
            Debug.LogWarning("MainLight not assigned!");
            return;
        }

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

            default: // High+
                QualitySettings.shadows = ShadowQuality.All;
                mainLight.shadows = LightShadows.Soft;
                break;
        }
    }

    // ---------------- RESOLUTION ----------------
    void SetResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;

        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolution", index);
    }

    // ---------------- VOLUME ----------------
    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    // ---------------- FPS ----------------
    void SetFpsMax(float value)
    {
        int fps = Mathf.RoundToInt(value);
        Application.targetFrameRate = fps;

        fpsText.text = fps.ToString();
        PlayerPrefs.SetInt("fps", fps);
    }
}