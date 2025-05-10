using System;
using UnityEngine;

public class InteractableMonitor : Interactable
{
    private GameObject mainCamera;
    private CameraManager cameraManager;
    public void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraManager = mainCamera.GetComponent<CameraManager>();
        
    }

    public override void Interact()
    {
        base.Interact();
        cameraManager?.TransitionCamera(new CameraStateMonitor(cameraManager));
    }
}
