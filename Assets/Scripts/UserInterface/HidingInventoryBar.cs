using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class HidingInventoryBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IHasRectTransform
{
    public RectTransform RectTransform => rect;

    [SerializeField]
    private RectTransform rect;

    private Coroutine hideRoutine;
    private bool isPresenting;

    private void Start()
    {
        hideRoutine = StartCoroutine(hide());
    }

    public void TriggerAlarm()
    {
        if (isPresenting)
            return;

        StartCoroutine(alarm());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(show());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hideRoutine = StartCoroutine(hide());
    }

    private IEnumerator show()
    {
        StopAllCoroutines();
        this.RectMoveTo(Vector3.zero, 0.5f, Easing.OutQuint);
        this.StartDelayedSchedule(() => isPresenting = true, 0.5f);

        yield return null;
    }

    private IEnumerator hide()
    {
        yield return new WaitForSeconds(1f);

        // 95%정도 숨겨 호버링 공간을 남김
        this.RectMoveTo(new Vector3(0, rect.rect.height * 0.9f, 0), 0.5f, Easing.OutQuint);
        this.StartDelayedSchedule(() => isPresenting = false, 0.5f);
    }

    private IEnumerator alarm()
    {
        this.RectMoveTo(new Vector3(0, rect.rect.height * 0f, 0), 0.25f, Easing.OutSine);

        yield return new WaitForSeconds(0.6f);

        this.RectMoveTo(new Vector3(0, rect.rect.height * 0.9f, 0), 0.5f, Easing.OutBounce);
    }
}
