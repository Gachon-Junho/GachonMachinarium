using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : AdjustableColor, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite ItemIcon
    {
        get => Image.sprite;
        set => Image.sprite = value;
    }

    public ItemInfo ItemInfo;

    public int Count
    {
        get => count;
        set
        {
            count = value;

            if (value == 0)
                inventory.Remove(this);

            if (!ItemInfo.IsStackable)
                return;

            ItemIcon = ItemInfo.StackInfo.FirstOrDefault(i => i.Count == value)?.Sprite ?? ItemIcon;
        }
    }

    public bool IsHovering { get; private set; }
    public bool DragStarted { get; private set; }
    public Item DraggingItem { get; private set; }

    [SerializeField]
    private RectTransform rect;

    private Vector3 initialPosition;
    private Inventory inventory;
    private int depth;
    private int count = 1;

    private (bool mergeable, ItemView to) mergeable = (false, null);

    private void Start()
    {
        initialPosition = rect.position;
        inventory ??= Inventory.Current;
    }

    public void Initialize(Inventory inventory, ItemInfo info = null)
    {
        this.inventory = inventory;
        ItemInfo ??= info;

        name = info?.Name ?? @"ItemView";
        ItemIcon = info?.ItemIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStarted = true;
        inventory.AlignElements = false;
        depth = transform.GetSiblingIndex();

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
        inventory.AlignElements = true;

        if (mergeable.mergeable)
            inventory.MergeItem(this, mergeable.to);
        else
            mergeable = (false, null);

        transform.SetSiblingIndex(depth);

        StopAllCoroutines();
        this.ColorTo(Color.white, 0.2f, Easing.Out);

        if (DraggingItem == null)
            return;

        bool success = DraggingItem.CheckSnappable();

        if (success)
        {
            Count--;
        }
        else
        {
            DraggingItem.MoveTo(initialPosition, 0.5f, Easing.OutQuint);
            DraggingItem.ColorTo(Color.clear, 0.5f, Easing.OutQuint);

            Destroy(DraggingItem.gameObject, 0.5f);
        }

        DraggingItem = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: 호버시 하이하이트 효과 (ex: 알파 조절)
        IsHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
    }

    public void CreateItem()
    {
        if (DragStarted && DraggingItem == null)
        {
            DraggingItem = Instantiate(ItemInfo.ItemPrefab).GetComponent<Item>();
            DraggingItem.transform.position = Camera.main!.DynamicScreenToWorldPoint(Input.mousePosition);
            DraggingItem.IsTrigger = true;
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
