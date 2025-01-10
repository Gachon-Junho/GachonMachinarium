using UnityEngine;

public class ItemSnapPoint : MonoBehaviour
{
    public ItemInfo TargetItem;

    public virtual void OnItemSnapped(Item item)
    {
        Destroy(gameObject);
    }
}
