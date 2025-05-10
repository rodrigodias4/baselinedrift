using NUnit.Framework;
using UnityEngine;

public class CameraStateTransition : CameraState
{
    public CameraStateStatic _previousState;
    public CameraStateStatic _nextState;
    private float timeElapsed = 0f;
    private float _cameraTransitionDuration;

    public CameraStateTransition(CameraManager cameraManager, CameraStateStatic previousState,
        CameraStateStatic newState) : base(cameraManager, CameraEnum.TransitionCamera)
    {
        canInteract = false;
        canLookAround = false;
        _cameraTransitionDuration = cameraManager.cameraTransitionDuration;
        _previousState = previousState as CameraStateStatic;
        _nextState = newState;
    }

    public override void Update()
    {
        Vector3 startPosition = _previousState._cameraTransform.position;
        Quaternion startRotation = _previousState._cameraTransform.rotation;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < _cameraTransitionDuration)
        {
            ContinueTransition();
        }
        else
        {
            FinishTransition();
        }
    }

    private void FinishTransition()
    {
        _cameraManager.MoveCamera(_nextState._cameraTransform.position);
        _cameraManager.RotateCamera(_previousState._cameraTransform.rotation);
        _cameraManager.ChangeCamera(_nextState);
    }

    public void ContinueTransition()
    {
        _cameraManager.MoveCamera(Vector3.Slerp(_previousState._cameraTransform.position,
            _nextState._cameraTransform.position,
            timeElapsed / _cameraTransitionDuration));
        ;
        _cameraManager.RotateCamera(Quaternion.Slerp(_previousState._cameraTransform.rotation,
            _nextState._cameraTransform.rotation,
            timeElapsed / _cameraTransitionDuration));
    }
}