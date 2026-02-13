using UnityEngine;

public class FlashLightBattery : MonoBehaviour
{
    public float BatteryLevel = 100f;
    public Light flashlight;
    public GameObject Lights;

    [Header("Intensity Settings")]
    public float maxIntensity = 2000f;
    public float minIntensity = 600f;

    [Header("Battery Settings")]
    public float drainRate = 0.2f;
    public float lowBatteryThreshold = 20f;

    [Header("Flicker Settings")]
    public float flickerAmount = 300f;
    public float flickerSpeed = 0.05f;

    float flickerTimer;

    void Update()
    {
        // Drain battery
        if (flashlight.enabled)
        {
            BatteryLevel -= drainRate * Time.deltaTime;
        }

        BatteryLevel = Mathf.Clamp(BatteryLevel, 0f, 100f);
        Lights.SetActive(BatteryLevel > 0);

        float batteryPercent = BatteryLevel / 100f;
        float baseIntensity = Mathf.Lerp(minIntensity, maxIntensity, batteryPercent);

        // LOW BATTERY → flicker (no smoothing)
        if (BatteryLevel <= lowBatteryThreshold && BatteryLevel > 0)
        {
            flickerTimer -= Time.deltaTime;

            if (flickerTimer <= 0f)
            {
                float flicker = Random.Range(-flickerAmount, flickerAmount);
                flashlight.intensity = Mathf.Clamp(
                    baseIntensity + flicker,
                    0f,
                    maxIntensity
                );

                flickerTimer = flickerSpeed;
            }
        }
        // NORMAL BATTERY → smooth dim
        else
        {
            flashlight.intensity = Mathf.Lerp(
                flashlight.intensity,
                baseIntensity,
                Time.deltaTime * 5f
            );
        }
    }
}
