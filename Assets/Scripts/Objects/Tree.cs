using UnityEngine;

public class Tree : InteractionableObject
{
    [SerializeField]
    private ItemInfo rewardItem;

    [SerializeField]
    private Vector3 itemSpawnPosition;

    private bool rewarded;

    protected override void StartInteraction()
    {
        Shake();

        if (ClickCount % 3 != 0)
            return;

        if (rewarded || rewardItem == null)
            return;

        var item = rewardItem.CreateItem();
        item.transform.position = itemSpawnPosition;
        item.Color = Color.clear;
        item.ColorTo(Color.white, 0.1f, Easing.OutQuint);

        rewarded = true;
    }
}
