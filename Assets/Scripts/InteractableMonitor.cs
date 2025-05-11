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
    public UnityEvent onEnterMonitorTask;
    public UnityEvent onExitMonitorTask;
    
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
    }

    public void DisableTask()
    {
        monitorVideo.clip = videoClips[0];
        task.SetActive(false);
        onExitMonitorTask?.Invoke();
    }

    public void EnableTask()
    {
        monitorVideo.clip = videoClips[1];
        task.SetActive(true);
        onEnterMonitorTask?.Invoke();
    }
}
