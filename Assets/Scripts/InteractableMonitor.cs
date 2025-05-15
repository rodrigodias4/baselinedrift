using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class InteractableMonitor : Interactable
{
    private GameObject mainCamera;
    private CameraManager cameraManager;
    [SerializeField] private GameObject task;
    [SerializeField] private VideoPlayer monitorVideo;
    [SerializeField] private List<VideoClip> videoClips;
    [SerializeField] private SkyLightManager skyLightManager;
    private TaskEyeMonitorPupilSine taskEyeMonitorPupilSine;
    
    public void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraManager = mainCamera.GetComponent<CameraManager>();
    }

    public override void Interact()
    {
        base.Interact();
        cameraManager?.TransitionCamera(new CameraStateMonitor(cameraManager));
        EnableTask();
        skyLightManager.Dim();
    }

    public void EnableTask()
    {
        monitorVideo.clip = videoClips[1];
        task.SetActive(true);
        taskEyeMonitorPupilSine = transform.GetComponentInChildren<TaskEyeMonitorPupilSine>();
        taskEyeMonitorPupilSine.SetTaskActive(true);
    }

    public void SetVideo(int video)
    {
        monitorVideo.clip = videoClips[video];
    }
}
