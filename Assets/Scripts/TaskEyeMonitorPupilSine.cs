using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum TaskEyeMonitorTuner
{
    Alpha,
    Beta,
    Gamma
}

public class TaskEyeMonitorPupilSine : MonoBehaviour
{
    [SerializeField] private GameObject eyePupilCube;
    [SerializeField] private int numPoints;
    public TaskEyeMonitorTuner currentTuner;
    List<GameObject> eyePupilCubeList = new List<GameObject>();
    private const float tau = Mathf.PI * 2f;
    private float timeElapsed;
    public float varianceAlphaFactor;
    public float varianceBetaFactor;
    public float varianceGammaFactor;
    public float factorRandomizer;
    public float frequencyAlpha;
    public float frequencyBeta;
    public float frequencyGamma;
    public float tunerSpeed;
    private float gammaAux = 9f;
    private float alphaAux = 5f;
    [SerializeField] private float radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (factorRandomizer > 0) RandomizeFactors();
        CreatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdatePoints();
        HandleInput();
    }

    void CreatePoints()
    {
        for (int i = 0; i < numPoints; i++)
        {
            eyePupilCubeList.Add(Instantiate(eyePupilCube, transform));
        }
    }

    void UpdatePoints()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float circle_x = Mathf.Cos((float)i / numPoints * tau);
            float circle_y = Mathf.Sin((float)i / numPoints * tau);
            float variance_gamma = Mathf.Sin(gammaAux * tau * (float)i / numPoints + timeElapsed * frequencyGamma * tau) *
                                   varianceGammaFactor;
            float variance_beta = Mathf.Sin(timeElapsed * frequencyBeta * tau) * varianceBetaFactor;
            float variance_alpha =
                Mathf.Sin(alphaAux * tau * (float)i / numPoints + timeElapsed * frequencyAlpha * tau) *
                varianceAlphaFactor;
            float x = circle_x * (radius + variance_alpha + variance_beta + variance_gamma);
            float y = circle_y * (radius + variance_alpha + variance_beta + variance_gamma);
            eyePupilCubeList[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentTuner != TaskEyeMonitorTuner.Alpha) currentTuner--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentTuner != TaskEyeMonitorTuner.Gamma) currentTuner++;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (currentTuner == TaskEyeMonitorTuner.Alpha) varianceAlphaFactor += tunerSpeed;
            if (currentTuner == TaskEyeMonitorTuner.Beta) varianceBetaFactor += tunerSpeed;
            if (currentTuner == TaskEyeMonitorTuner.Gamma) varianceGammaFactor += tunerSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (currentTuner == TaskEyeMonitorTuner.Alpha) varianceAlphaFactor -= tunerSpeed;
            if (currentTuner == TaskEyeMonitorTuner.Beta) varianceBetaFactor -= tunerSpeed;
            if (currentTuner == TaskEyeMonitorTuner.Gamma) varianceGammaFactor -= tunerSpeed;
        }
    }

    private void RandomizeFactors()
    {
        varianceAlphaFactor *= Random.Range(1f - factorRandomizer, 1f + factorRandomizer);
        varianceBetaFactor *= Random.Range(1f - factorRandomizer, 1f + factorRandomizer);
        varianceGammaFactor *= Random.Range(1f - factorRandomizer, 1f + factorRandomizer);
        gammaAux *= Random.Range(0.7f, 1.5f);
        alphaAux *= Random.Range(0.7f, 1.5f);
    }
}