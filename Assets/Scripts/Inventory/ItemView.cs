using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite ItemIcon;
    
    [SerializeField] 
    private RectTransform rect;
    
    [SerializeField] 
    private Image image;

    [SerializeField] 
    public GameObject Item;

    private bool dragStarted;
    private Vector3 initialPosition;
    private Item dragging;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = rect.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStarted = true;
        initialPosition = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging == null)
            return;
        
        dragging.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var success = dragging?.OnItemDropped();
        
        Debug.Log($"{name} drag stopped");

        dragStarted = false;
        dragging = null;

        // TODO: 지정된 위치에 퍼즐이 배치되었자면 인벤토리 아이템 삭제, 그렇지 않다면 퍼즐 삭제 (macOS 애니메이션 참조)
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: 호버시 하이하이트 효과 (ex: 알파 조절)
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragStarted && dragging == null)
        {
            dragging = Instantiate(Item).GetComponent<Item>();
            dragging.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
        }
    }
}
