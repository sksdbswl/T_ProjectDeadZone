using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0f, 1f)]
    public float time;
    public float fullDayLength = 120f;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private const float DayStart = 0.25f;
    private const float NightStart = 0.75f;

    private bool isDayTime;
    private Material skyboxMaterial;

    public void Initialize()
    {
        skyboxMaterial = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyboxMaterial;

        timeRate = 1.0f / fullDayLength;
        time = startTime;

        UpdateSunReference();
        UpdateSkyboxExposure();

    }

    private void FixedUpdate()
    {
        UpdateTime();
        UpdateLighting(sun, sunColor, sunIntensity, DayStart);
        UpdateLighting(moon,moonColor,moonIntensity, NightStart);
        UpdateEnvironmentLighting();
        CheckAndUpdateDayNightCycle();
    }


    private void UpdateTime()
    {
        time = (time + timeRate * Time.fixedDeltaTime) % 1.0f;
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve, float timeOffset)
    {
        float adjustedTime = (time - timeOffset) * 4.0f;
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = adjustedTime * noon;
        lightSource.color = colorGradient.Evaluate(time);
        lightSource.intensity = intensity;

        bool shouldBeActive = intensity > 0;
        if (lightSource.gameObject.activeSelf != shouldBeActive)
        {
            lightSource.gameObject.SetActive(shouldBeActive);
        }
    }

    private void UpdateEnvironmentLighting()
    {
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    private void CheckAndUpdateDayNightCycle()
    {
        bool newDaytimeStatus = time>=DayStart && time <= NightStart;
        if (newDaytimeStatus != isDayTime)
        {
            isDayTime = newDaytimeStatus;
            UpdateSunReference();
            UpdateSkyboxExposure();
        }
    }

    private void UpdateSunReference()
    {
        RenderSettings.sun = isDayTime ? sun : moon;
    }

    private void UpdateSkyboxExposure()
    {
        if (RenderSettings.skybox == null) return;

        if (RenderSettings.skybox.HasProperty("_Exposure"))
        {
            float targetExposure = isDayTime ? 1.3f : 0.3f;
            RenderSettings.skybox.SetFloat("_Exposure", targetExposure);
        }

        if (RenderSettings.skybox.HasProperty("_AtmosphereThickness"))
        {
            float targetThickness = isDayTime ? 1f : 0.1f;
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", targetThickness);
        }
    }

}
