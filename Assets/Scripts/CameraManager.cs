using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum CameraEnum
{
    PlayerCamera,
    MonitorCamera,
    TransitionCamera,
}

public class CameraManager : MonoBehaviour
{
    public float mouseSensitivity;
    public float cameraTransitionDuration;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject monitorCamera;
    public CameraState currentCameraState;
    public Dictionary<CameraEnum, GameObject> cameraTransforms;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = new Vector3(playerCamera.transform.position.x,
            playerCamera.transform.position.y, transform.position.z);
        cameraTransforms = new Dictionary<CameraEnum, GameObject>
        {
            { CameraEnum.PlayerCamera, playerCamera },
            { CameraEnum.MonitorCamera, monitorCamera },
        };
        currentCameraState = new CameraStatePlayer(this);
        currentCameraState.EnterState();
    }

    void Update()
    {
        currentCameraState.Update();
    }

    public void MoveCamera(Vector3 position)
    {
        transform.position = position;
    }

    public void RotateCamera(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public void SetCameraState(CameraState cameraState)
    {
        currentCameraState = cameraState;
    }

    // Redundant function but keeping if we need to add more than state machine transitions
    public void ChangeCamera(CameraState newCameraState)
    {
        currentCameraState?.ExitState();
        SetCameraState(newCameraState);
        currentCameraState?.EnterState();
    }

    public void TransitionCamera(CameraState newCameraState)
    {
        ChangeCamera(new CameraStateTransition(this, currentCameraState as CameraStateStatic,
            newCameraState as CameraStateStatic));
    }
}