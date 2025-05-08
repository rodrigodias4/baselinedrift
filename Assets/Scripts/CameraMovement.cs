using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 4f;
    [SerializeField] private Transform playerTransform;

    private float xRotation = 0f;
    private float yRotation = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100 * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, 0, 180f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}