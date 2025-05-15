using UnityEngine;

public enum TaskEyeMonitorTunerKnobType
{
    Alpha,
    Beta,
    Gamma,
    None
}

public class TaskEyeMonitorTunerKnob : MonoBehaviour
{
    [SerializeField] private Material tunerMaterial;
    private Material frontMaterial;
    private MeshRenderer meshRenderer;
    public TaskEyeMonitorTunerKnobType type;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        frontMaterial = meshRenderer.materials[1];
    }

    public void SetActiveMaterial()
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = tunerMaterial;
        meshRenderer.materials = materials;
    }

    public void SetInactiveMaterial()
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = frontMaterial;
        meshRenderer.materials = materials;
    }
}
