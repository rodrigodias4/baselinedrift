using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class InteractableDecision : Interactable
{
    protected MeshRenderer meshRenderer;
    protected Color defaultColor;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Assert.IsNotNull(meshRenderer);
        defaultColor = meshRenderer.material.color;
        SetColor(Color.black);
    }

    public override void DisplayHint()
    {
        SetColor(defaultColor);
        base.DisplayHint();
    }

    public override void HideHint()
    {
       SetColor(Color.black);
        base.HideHint();
    }

    private void SetColor(Color color)
    {
        meshRenderer.material.SetColor("_EmissionColor", color);
    }
}
