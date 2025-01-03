using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    private GameObject content;

    [SerializeField] 
    private GameObject itemView;
    
    [SerializeField] 
    private int maxCapacity = 9;
    
    private List<ItemView> items = new List<ItemView>();
    private HorizontalLayoutGroup layout;

    public bool AlignElements
    {
        get => layout.enabled;
        set
        {
            if (layout.enabled == value)
                return;

            layout.enabled = value;
        }
    }
    
    private void Start()
    {
        items = content.GetComponentsInChildren<ItemView>().ToList();
        layout = content.GetComponent<HorizontalLayoutGroup>();
    }

    public void Add(Item item)
    {
        if (items.Count >= maxCapacity)
            return;

        var newView = Instantiate(itemView, content.transform).GetComponent<ItemView>();
        
        newView.Initialize(this, item.Info);
        items.Add(newView);
    }

    public void Remove(ItemView item)
    {
        var target = items.FirstOrDefault(i => ReferenceEquals(item, i));

        if (target == null)
            return;

        items.Remove(item);
        Destroy(item.gameObject);
    }

    public bool MergeItem(ItemView from, ItemView to)
    {
        // TODO: 아이템 병합 성공시 true, 실패시 false. 아이템 드래그는 여기서 처리하지 않음.
        
        return true;
    }

    public (bool mergeable, ItemView to) CheckMargeable(ItemView from)
    {
        var to = items.FirstOrDefault(v => !ReferenceEquals(from, v) && v.IsHoverring);
        
        // TODO: 조합표에서 찾아서 병합가능 여부 확인.
        
        return (false, to);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var holding = items.FirstOrDefault(v => v.DragStarted && v.DraggingItem != null);

        if (holding == null)
            return;
        
        holding.DestroyItem();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var holding = items.FirstOrDefault(v => v.DragStarted);

        if (holding == null)
            return;

        holding.CreateItem();
    }
}