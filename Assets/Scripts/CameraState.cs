using UnityEngine;

public abstract class CameraState
{
    protected CameraManager _cameraManager;
    public bool canLookAround = false;
    public bool canInteract = false;
    public CameraEnum _cameraEnum;

    public CameraState(CameraManager cameraManager, CameraEnum cameraEnum)
    {
        _cameraManager = cameraManager;
        _cameraEnum = cameraEnum;
    }
    

    public virtual void Update() {}

    public virtual void ExitState() {}
    
    public virtual void EnterState() {}
}
