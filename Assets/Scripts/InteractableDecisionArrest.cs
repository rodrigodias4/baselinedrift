using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InteractableDecisionArrest : InteractableDecision
{
    public UnityEvent onSuspectArrest;
    public override void Interact()
    {
        base.Interact();
        onSuspectArrest?.Invoke();
    }
}
