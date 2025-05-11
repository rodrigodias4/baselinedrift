using UnityEngine;
using UnityEngine.Assertions;

public class CameraStatePlayer : CameraStateStatic
{
    private float xRotation = 0f;
    private float yRotation = 90f;
    private float mouseSensitivity;
    
    public CameraStatePlayer(CameraManager cameraManager) : base(cameraManager,
        cameraManager.cameraTransforms[CameraEnum.PlayerCamera].transform, CameraEnum.PlayerCamera)
    {
        canInteract = true;
        canLookAround = true;
        this.mouseSensitivity = cameraManager.mouseSensitivity;
        _cameraTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public override void Update()
    {
        HandleMouseLook();
    }
    
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100 * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, 0, 180f);

        _cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        _cameraManager.RotateCamera(_cameraTransform.rotation);
    }
}