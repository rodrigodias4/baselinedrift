using UnityEngine;

public class InteractableNotepad : Interactable
{
    public override void Interact()
    {
        base.Interact();
        cameraManager?.TransitionCamera(new CameraStateNotepad(cameraManager));
    }
}
