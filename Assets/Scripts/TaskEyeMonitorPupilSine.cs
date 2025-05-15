using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;



public class TaskEyeMonitorPupilSine : MonoBehaviour
{
    [SerializeField] private GameObject taskInProgressScreen;
    [SerializeField] private GameObject taskLockedScreen;
    [SerializeField] private GameObject eyePupilCube;
    [SerializeField] private int numPoints;
    public TaskEyeMonitorTunerKnob currentTuner;
    public TaskEyeMonitorTunerKnob[] tuners;
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
    private bool isLocked = false;
    [SerializeField] private float radius;
    [SerializeField] private float maxFactor;

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
            x = Mathf.Clamp(x, -2 * radius, 2 * radius);
            float y = circle_y * (radius + variance_alpha + variance_beta + variance_gamma);
            y = Mathf.Clamp(y, -2 * radius, 2 * radius);
            eyePupilCubeList[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }

    void HandleInput()
    {
        if (isLocked) return;
        
        if (Input.GetKeyDown(KeyCode.A) || (Input.mouseScrollDelta[1] < 0))
        {
            if (currentTuner.type > TaskEyeMonitorTunerKnobType.Alpha) SetActiveTuner(currentTuner.type - 1);
        }

        if (Input.GetKeyDown(KeyCode.D) || (Input.mouseScrollDelta[1] > 0))
        {
            if (currentTuner.type < TaskEyeMonitorTunerKnobType.Gamma) SetActiveTuner(currentTuner.type + 1);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetMouseButton(0))
        {
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Alpha) AddToAlpha(tunerSpeed);
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Beta) AddToBeta(tunerSpeed);
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Gamma) AddToGamma(tunerSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetMouseButton(1))
        {
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Alpha) AddToAlpha(-tunerSpeed);
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Beta) AddToBeta(-tunerSpeed);
            if (currentTuner.type == TaskEyeMonitorTunerKnobType.Gamma) AddToGamma(-tunerSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LockBaseline();
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

    private void SetActiveTuner(TaskEyeMonitorTunerKnobType type)
    {
        //Debug.Log("Active tuner: " + type);
        currentTuner?.SetInactiveMaterial();
        if (type != TaskEyeMonitorTunerKnobType.None)
        {
            currentTuner = tuners[(int)type].GetComponent<TaskEyeMonitorTunerKnob>();
            currentTuner.SetActiveMaterial();
        }
        else
        {
            currentTuner = null;
        }
    }

    public void AddToAlpha(float value)
    {
        varianceAlphaFactor = Mathf.Clamp(varianceAlphaFactor + value, -maxFactor, maxFactor);
    }

    public void AddToBeta(float value)
    {
        varianceBetaFactor = Mathf.Clamp(varianceBetaFactor + value, -maxFactor, maxFactor);
    }

    public void AddToGamma(float value)
    {
        varianceGammaFactor = Mathf.Clamp(varianceGammaFactor + value, -maxFactor, maxFactor);
    }

    public void LockBaseline()
    {
        isLocked = true;
        SetActiveTuner(TaskEyeMonitorTunerKnobType.None);
        taskInProgressScreen.SetActive(false);
        taskLockedScreen.SetActive(true);
        Invoke(nameof(HideLockedText), 2);
    }

    public void HideLockedText()
    {
        taskLockedScreen.SetActive(false);
    }

    public void SetTaskActive(bool value)
    {
        taskInProgressScreen.SetActive(value && !isLocked);
        SetActiveTuner(value ? TaskEyeMonitorTunerKnobType.Alpha : TaskEyeMonitorTunerKnobType.None);
    }
}