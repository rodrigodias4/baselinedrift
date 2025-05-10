using UnityEngine;
using UnityEngine.Assertions;

public class Interactor : MonoBehaviour
{
    private Interactable currentInteractable = null;
    private Camera camera;
    private CameraManager _cameraManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        Assert.IsNotNull(camera);
        _cameraManager = GetComponent<CameraManager>();
        Assert.IsNotNull(_cameraManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(_cameraManager?.currentCameraState?.canInteract ?? false)) return;
        InteractableCheck();
        InteractableDisplayHint();
        if (Input.GetKeyDown(KeyCode.E)) InteractableInteract();
    }

    private void InteractableCheck()
    {
        Ray r = camera.ViewportPointToRay(Vector3.one * 0.5f);
        if (Physics.Raycast(r, out RaycastHit hit))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                currentInteractable = interactable;
                return;
            }
        }
        currentInteractable?.HideHint();
        currentInteractable = null;
    }

    private void InteractableDisplayHint()
    {
        currentInteractable?.DisplayHint();
    }

    private void InteractableInteract()
    {
        currentInteractable?.Interact();
    }
}
