using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected CameraManager cameraManager;
    protected GameObject mainCamera;
    public GameObject hint;
    public UIInteractHint hintUI;

    protected void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraManager = mainCamera.GetComponent<CameraManager>();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        HideHint();
    }

    public virtual void DisplayHint()
    {
        if (hint) hint.SetActive(true);
        if (hintUI) hintUI.Activate();
    }

    public virtual void HideHint()
    {
        if (hint) hint.SetActive(false);
        if (hintUI) hintUI.Deactivate();
    }
}