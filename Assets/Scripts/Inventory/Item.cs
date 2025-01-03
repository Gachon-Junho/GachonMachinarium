using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IHasColor
{
    public virtual Color Color
    {
        get => sprite.color;
        set => sprite.color = value;
    }

    [SerializeField]
    private Collider collider;

    [SerializeField]
    private SpriteRenderer sprite;

    public ItemInfo Info;

    private bool dragging;
    private RaycastHit hit;

    private void Start()
    {
        collider.enabled = false;
    }

    public bool OnItemDropped()
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            if (!hit.collider.CompareTag("SnapPoint"))
                return false;

            // 스냅포인트 지점에 원하지 않는 아이템은 필터함.
            if (hit.collider.gameObject.GetComponent<ItemSnapPoint>().TargetType != Info.Type)
                return false;

            Destroy(hit.collider.gameObject);
            this.MoveTo(hit.collider.transform.position, 1f, Easing.OutQuint);
            this.StartDelayedSchedule(() => collider.enabled = true, 1);

            return true;
        }

        return false;
    }
}
