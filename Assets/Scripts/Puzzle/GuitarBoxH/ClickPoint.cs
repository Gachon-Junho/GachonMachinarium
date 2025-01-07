using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickPoint : MonoBehaviour, IHasColor
{
    public Color Color
    {
        get => image.color;
        set => image.color = value;
    }

    [SerializeField] private Image image;

    public event Action<ClickPoint, Collision> CollisionEnter;
    public event Action<ClickPoint, Collision> CollisionExit;

    protected virtual void OnCollisionEnter(Collision other)
    {
        // 같은 클릭포인트의 충돌은 무시
        if (other.gameObject.GetComponent<ClickPoint>() != null)
            return;

        CollisionEnter?.Invoke(this, other);
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        // 같은 클릭포인트의 충돌은 무시
        if (other.gameObject.GetComponent<ClickPoint>() != null)
            return;

        CollisionExit?.Invoke(this, other);
    }
}
