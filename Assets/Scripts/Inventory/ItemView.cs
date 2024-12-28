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
    public GameObject item;

    private bool dragStarted;
    private Vector3 initialPosition;
    private Item dragging;

    private Inventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = rect.position;

        inventory ??= Inventory.Current;
    }

    public void Initialize(Inventory inventory, ItemInfo info)
    {
        this.inventory = inventory;
        item = info.ItemPrefab;
        ItemIcon = info.ItemIcon;
        image.sprite = ItemIcon;
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
        if (dragging == null)
            return;
        
        var success = dragging.OnItemDropped();

        if (success)
        {
            inventory.Remove(this);
        }
        else
        {
            dragging.MoveTo(initialPosition, 0.5f, Easing.OutQuint);
            Destroy(dragging.gameObject, 0.5f);
        }

        dragStarted = false;
        dragging = null;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: 호버시 하이하이트 효과 (ex: 알파 조절)
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragStarted && dragging == null)
        {
            dragging = Instantiate(item).GetComponent<Item>();
            dragging.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
        }
    }
}
