using UnityEngine;
using UnityEngine.UI;

public class FlashLightBattery : MonoBehaviour
{
    public float BatteryLevel = 100f;
    public Light flashlight;
    public GameObject Lights;

    [Header("Settings")]
    public float drainRate = 0.2f;
    public float lowBatteryThreshold = 20f;
    public float maxIntensity = 2000f;
    public float minIntensity = 600f;

    [Header("Flicker")]
    public float flickerAmount = 300f;
    public float flickerSpeed = 0.05f;

    [Header("UI Icons (Drop in order 1-5)")]
    public RawImage[] batteryIcons; 

    private float flickerTimer;
    private int lastIconCount = -1; 

    void Update()
    {
       
        if (flashlight.enabled && BatteryLevel > 0)
        {
            BatteryLevel -= drainRate * Time.deltaTime;
            BatteryLevel = Mathf.Max(BatteryLevel, 0f);

            
            UpdateUI();
        }

        HandleFlashlightVisuals();
    }

    void UpdateUI()
    {
      
        int currentIconCount = Mathf.CeilToInt(BatteryLevel / 20f);

       
        if (currentIconCount != lastIconCount)
        {
            for (int i = 0; i < batteryIcons.Length; i++)
            {
                batteryIcons[i].enabled = i < currentIconCount;
            }
            lastIconCount = currentIconCount;

         
            Lights.SetActive(BatteryLevel > 0);
        }
    }

    void HandleFlashlightVisuals()
    {
        float batteryPercent = BatteryLevel / 100f;
        float baseIntensity = Mathf.Lerp(minIntensity, maxIntensity, batteryPercent);

        if (BatteryLevel <= lowBatteryThreshold && BatteryLevel > 0)
        {
            flickerTimer -= Time.deltaTime;
            if (flickerTimer <= 0f)
            {
                flashlight.intensity = baseIntensity + Random.Range(-flickerAmount, flickerAmount);
                flickerTimer = flickerSpeed;
            }
        }
        else
        {
            flashlight.intensity = Mathf.Lerp(flashlight.intensity, baseIntensity, Time.deltaTime * 5f);
        }
    }
}