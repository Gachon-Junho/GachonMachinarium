using System;
using UnityEngine;

public class HomeDropArea : MonoBehaviour
{
    [SerializeField]
    private ItemInfo itemInfo;

    [SerializeField]
    private float spawnPositionY;

    private bool rewarded;

    private void OnTriggerEnter(Collider other)
    {
        var home = other.gameObject.GetComponent<Home>();

        if (home == null || rewarded)
            return;

        var item = itemInfo.CreateItem();
        item.transform.position = new Vector3(home.transform.position.x, spawnPositionY);
        item.Color = Color.clear;
        item.ColorTo(Color.white, 0.5f, Easing.OutQuint);

        Destroy(home.gameObject, 0.5f);
        rewarded = true;
        // TODO: Home의 이미지 페이드아웃
    }
}
