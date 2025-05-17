using System;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected CameraManager cameraManager;
    protected GameObject mainCamera;
    public GameObject hint;
    public UIInteractHint hintUI;
    public string hintText;
    private string hintDefaultText = "Interact";

    protected void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraManager = mainCamera.GetComponent<CameraManager>();
        if (hintText == "") hintText = hintDefaultText;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        HideHint();
    }

    public virtual void DisplayHint()
    {
        if (hint) hint.SetActive(true);
        if (hintUI)
        {
            hintUI.SetText(hintText);
            hintUI.Activate();
        }
    }

    public virtual void HideHint()
    {
        if (hint) hint.SetActive(false);
        if (hintUI) hintUI.Deactivate();
    }
}