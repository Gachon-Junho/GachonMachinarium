using System;
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

    private void Start()
    {
        hideRoutine = StartCoroutine(hide());
    }

    public void TriggerAlarm()
    {
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

        yield return null;
    }

    private IEnumerator hide()
    {
        yield return new WaitForSeconds(1f);
        
        // 95%정도 숨겨 호버링 공간을 남김
        this.RectMoveTo(new Vector3(0, rect.rect.height * 0.9f, 0), 0.5f, Easing.OutQuint);
    }

    private IEnumerator alarm()
    {
        this.RectMoveTo(new Vector3(0, rect.rect.height * 0.6f, 0), 0.25f, Easing.OutQuint);

        yield return new WaitForSeconds(0.5f);
        
        this.RectMoveTo(new Vector3(0, rect.rect.height * 0.9f, 0), 0.25f, Easing.OutQuint);
    }
}