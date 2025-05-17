using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class InteractableMonitor : Interactable
{
    [SerializeField] private GameObject task;
    [SerializeField] private VideoPlayer monitorVideo;
    [SerializeField] private List<VideoClip> videoClips;
    [SerializeField] private SkyLightManager skyLightManager;
    private TaskEyeMonitorPupilSine taskEyeMonitorPupilSine;

    public override void Interact()
    {
        base.Interact();
        cameraManager?.TransitionCamera(new CameraStateMonitor(cameraManager));
        EnableTask();
        skyLightManager.Dim();
    }

    public void EnableTask()
    {
        SetVideo(1);
        task.SetActive(true);
        taskEyeMonitorPupilSine = transform.GetComponentInChildren<TaskEyeMonitorPupilSine>();
        taskEyeMonitorPupilSine.SetTaskActive(true);
    }
    
    public void DisableTask()
    {
        SetVideo(0);
        taskEyeMonitorPupilSine = transform.GetComponentInChildren<TaskEyeMonitorPupilSine>();
        taskEyeMonitorPupilSine?.SetTaskActive(false);
        task.SetActive(false);
    }

    public void SetVideo(int video)
    {
        monitorVideo.clip = videoClips[video];
    }
}
