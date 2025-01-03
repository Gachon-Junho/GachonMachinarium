using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IHasColor, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite ItemIcon;

    public Color Color
    {
        get => image.color;
        set => image.color = value;
    }
    
    [SerializeField] 
    private RectTransform rect;
    
    [SerializeField] 
    private Image image;

    [SerializeField]
    public GameObject item;

    public bool IsHoverring;
    public bool DragStarted;
    public Item DraggingItem;
    
    private Vector3 initialPosition;
    private Inventory inventory;
    private int depth;

    private (bool mergeable, ItemView to) mergeable = (false, null);
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = rect.position;
        inventory ??= Inventory.Current;
        depth = transform.GetSiblingIndex();
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
        DragStarted = true;
        inventory.AlignElements = false;
        transform.SetAsFirstSibling();
        initialPosition = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);

        StopAllCoroutines();
        this.ColorTo(Color.gray, 0.2f, Easing.Out);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        if (DraggingItem == null)
        {
            checkMergeable();
            return;
        }
        
        StopAllCoroutines();
        this.ColorTo(Color.gray, 0.2f, Easing.Out);

        mergeable = (false, null);
        DraggingItem.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
    }

    private void checkMergeable()
    {
        var newMergeable = inventory.CheckMargeable(this);
        
        if (mergeable == newMergeable)
            return;
        
        if (newMergeable.mergeable || newMergeable.to == null)
        {
            StopAllCoroutines();
            this.ColorTo(Color.gray, 0.2f, Easing.Out);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(alarmNotMergeable());
        }

        mergeable = newMergeable;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragStarted = false;
        mergeable = (false, null);
        inventory.AlignElements = true;
        
        transform.SetSiblingIndex(depth);
        
        StopAllCoroutines();
        this.ColorTo(Color.white, 0.2f, Easing.Out);
        
        if (DraggingItem == null)
            return;
        
        var success = DraggingItem.OnItemDropped();

        if (success)
        {
            inventory.Remove(this);
        }
        else
        {
            DraggingItem.MoveTo(initialPosition, 0.5f, Easing.OutQuint);
            Destroy(DraggingItem.gameObject, 0.5f);
        }
        
        DraggingItem = null;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: 호버시 하이하이트 효과 (ex: 알파 조절)
        IsHoverring = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        IsHoverring = false;
    }

    public void CreateItem()
    {
        if (DragStarted && DraggingItem == null)
        {
            DraggingItem = Instantiate(item).GetComponent<Item>();
            DraggingItem.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void DestroyItem()
    {
        if (DraggingItem == null)
            return;
        
        Destroy(DraggingItem.gameObject);
        DraggingItem = null;
    }

    private IEnumerator alarmNotMergeable()
    {
        while (true)
        {
            this.ColorTo(Color.red * 0.5f, 0.5f, Easing.Out);
            
            yield return new WaitForSeconds(0.5f);
    
            this.ColorTo(Color.gray, 0.5f, Easing.Out);
    
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
}
