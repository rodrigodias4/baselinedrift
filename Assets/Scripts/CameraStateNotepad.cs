using UnityEngine;

public class CameraStateNotepad : CameraStateStatic
{
    public CameraStateNotepad(CameraManager cameraManager) : base(cameraManager,
        cameraManager.cameraTransforms[CameraEnum.NotepadCamera].transform, CameraEnum.MonitorCamera)
    {
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _cameraManager.ChangeCamera(new CameraStateTransition(_cameraManager, this,
                new CameraStatePlayer(_cameraManager)));
        }
    }

    public override void ExitState()
    {
    }
}