using UnityEngine;

public class CameraStateMonitor : CameraStateStatic
{
    public CameraStateMonitor(CameraManager cameraManager) : base(cameraManager,
        cameraManager.cameraTransforms[CameraEnum.MonitorCamera].transform, CameraEnum.MonitorCamera)
    {
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _cameraManager.ChangeCamera(new CameraStateTransition(_cameraManager, this,
                new CameraStatePlayer(_cameraManager)));
    }

    public override void ExitState()
    {
    }
}