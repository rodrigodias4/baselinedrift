using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject hint;
    public UIInteractHint hintUI;

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