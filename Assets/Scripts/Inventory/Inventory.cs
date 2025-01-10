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

    [SerializeField]
    private ItemCombinationTable itemTable;

    [SerializeField]
    private HidingInventoryBar hiding;

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

    public ItemView Add(ItemInfo item)
    {
        if (item.IsStackable)
        {
            var to = items.FirstOrDefault(i => i.ItemInfo.Equals(item));

            if (to != null)
            {
                to.Count++;
                hiding.TriggerAlarm();

                return to;
            }
        }

        if (items.Count >= maxCapacity)
            return null;

        var newView = item.CreateItemView(content.transform);
        newView.Initialize(this, item);

        items.Add(newView);
        hiding.TriggerAlarm();

        return newView;
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
        var combination = itemTable.Table.FirstOrDefault(c => c.First == from.ItemInfo && c.Second == to.ItemInfo ||
                                            c.First == to.ItemInfo && c.Second == from.ItemInfo);

        if (combination == null)
            return false;

        int depth = to.transform.GetSiblingIndex();

        from.Count--;
        to.Count--;

        var merged = Add(combination.Result);
        merged.transform.SetSiblingIndex(depth);

        return true;
    }

    public (bool mergeable, ItemView to) CheckMergeable(ItemView from)
    {
        var to = items.FirstOrDefault(v => !ReferenceEquals(from, v) && v.IsHovering);

        if (to == null)
            return (false, to);

        var combination = itemTable.Table.FirstOrDefault(c => c.First == from.ItemInfo && c.Second == to.ItemInfo ||
                                                              c.First == to.ItemInfo && c.Second == from.ItemInfo);

        return (combination != null, to);
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
