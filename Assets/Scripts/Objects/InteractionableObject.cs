using System.Collections;
using UnityEngine;

public class InteractionableObject : MonoBehaviour
{
    protected bool Clicked { get; private set; }
    protected int ClickCount { get; private set; }

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void StartInteraction()
    {
    }

    protected virtual void OnMouseDown()
    {
        if (Clicked)
            return;

        Clicked = true;
        ClickCount++;

        StartInteraction();
    }

    private void OnMouseUp()
    {
        Clicked = false;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(shake());
    }

    private IEnumerator shake()
    {
        const float shake_duration = 0.08f;
        Vector3 shakeMagnitude = new Vector3(0.25f, 0.05f, 0);

        this.MoveTo(initialPosition + shakeMagnitude, shake_duration / 2, Easing.OutSine);
        yield return new WaitForSeconds(shake_duration / 2);

        this.MoveTo(initialPosition - shakeMagnitude, shake_duration, Easing.InOutSine);
        yield return new WaitForSeconds(shake_duration);

        this.MoveTo(initialPosition + shakeMagnitude, shake_duration, Easing.InOutSine);
        yield return new WaitForSeconds(shake_duration);

        this.MoveTo(initialPosition - shakeMagnitude, shake_duration, Easing.InOutSine);
        yield return new WaitForSeconds(shake_duration);

        this.MoveTo(initialPosition, shake_duration / 2, Easing.InSine);
        yield return new WaitForSeconds(shake_duration / 2);
    }
}
