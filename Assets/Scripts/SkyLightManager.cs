using System.Collections;
using UnityEngine;

public class SkyLightManager : MonoBehaviour
{
    public float dimAmount;
    public float dimFadeDuration;
    private Light skylight;
    private float defaultSkyLightIntensity;

    public void Start()
    {
        skylight = GetComponent<Light>();
        defaultSkyLightIntensity = skylight.intensity;
    }
    
    public void Dim()
    {
        StartCoroutine(FadeToValue(dimAmount * skylight.intensity));
    }

    public void Undim()
    {
        StartCoroutine(FadeToValue(defaultSkyLightIntensity));
    }

    private IEnumerator FadeToValue(float value)
    {
        float timeElapsed = 0;
        float startValue = skylight.intensity;

        while (timeElapsed < dimFadeDuration)
        {
            timeElapsed += Time.deltaTime;
            skylight.intensity = Mathf.Lerp(startValue, value, timeElapsed / dimFadeDuration);
            yield return null;
        }
        
        skylight.intensity = value;
    }
}
