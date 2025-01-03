using UnityEngine;

public class MeshItem : Item
{
    public override Color Color
    {
        get => MeshRenderer.material.color;
        set => MeshRenderer.material.color = value;
    }

    protected MeshRenderer MeshRenderer => meshRenderer ??= GetComponent<MeshRenderer>();

    private MeshRenderer meshRenderer;
}
