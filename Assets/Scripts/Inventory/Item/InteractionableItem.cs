using System;
using System.Collections;
using UnityEngine;

public class InteractionableItem : MonoBehaviour
{
    [SerializeField]
    private ItemInfo rewardItem;

    [SerializeField]
    private Vector3 itemSpawnPosition;

    private Vector3 initialPosition;

    private bool clicked;
    private int clickCount;
    private bool rewarded;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (clicked)
            return;

        clicked = true;
        clickCount++;

        StopAllCoroutines();
        StartCoroutine(shake());

        if (clickCount % 3 != 0)
            return;

        if (rewarded)
            return;

        var item = rewardItem.CreateItem();
        item.transform.position = itemSpawnPosition;
        item.Color = Color.clear;
        item.ColorTo(Color.white, 0.1f, Easing.OutQuint);

        rewarded = true;
    }

    private void OnMouseUp()
    {
        clicked = false;
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
