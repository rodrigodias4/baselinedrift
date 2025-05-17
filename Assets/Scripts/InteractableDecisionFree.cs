using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InteractableDecisionFree : InteractableDecision
{
    public UnityEvent onSuspectFree;
    public override void Interact()
    {
        base.Interact();
        onSuspectFree?.Invoke();
    }
}
