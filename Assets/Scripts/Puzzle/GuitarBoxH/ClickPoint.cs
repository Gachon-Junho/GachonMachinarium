using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickPoint : AdjustableColor
{
    public event Action<ClickPoint, Collider2D> CollisionEnter;
    public event Action<ClickPoint, Collider2D> CollisionExit;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        CollisionEnter?.Invoke(this, other);
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        CollisionExit?.Invoke(this, other);
    }
}
