using UnityEngine;

public class CameraStateStatic : CameraState
{
    public Transform _cameraTransform { get; protected set; }

    public CameraStateStatic(CameraManager cameraManager, Transform cameraTransform, CameraEnum cameraEnum) : base(cameraManager, cameraEnum)
    {
        canLookAround = false;
        canInteract = false;
        _cameraTransform = cameraTransform;
    }

    public override void EnterState()
    {
        _cameraManager.MoveCamera(_cameraTransform.position);
        _cameraManager.RotateCamera(_cameraTransform.rotation);
    }
}
