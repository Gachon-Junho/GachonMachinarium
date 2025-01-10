using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour, IHasColor
{
    public virtual Color Color
    {
        get => sprite.color;
        set => sprite.color = value;
    }

    public bool IsTrigger
    {
        get => Collider.isTrigger;
        set => Collider.isTrigger = value;
    }

    [FormerlySerializedAs("collider")]
    [SerializeField]
    protected Collider Collider;

    [SerializeField]
    private SpriteRenderer sprite;

    public ItemInfo Info;

    private bool dragging;
    private RaycastHit hit;

    public bool CheckSnappable()
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            if (!hit.collider.CompareTag("SnapPoint"))
                return false;

            // 스냅포인트 지점에 원하지 않는 아이템은 필터함.
            if (!hit.collider.gameObject.GetComponent<ItemSnapPoint>().TargetItem.Equals(Info))
                return false;

            OnItemDropped(hit);

            return true;
        }

        return false;
    }

    protected virtual void OnItemDropped(RaycastHit snapPoint)
    {
        Destroy(snapPoint.collider.gameObject, 1);
        this.MoveTo(snapPoint.collider.transform.position, 1f, Easing.OutQuint);
        this.StartDelayedSchedule(() => Collider.isTrigger = false, 1);
    }
}
