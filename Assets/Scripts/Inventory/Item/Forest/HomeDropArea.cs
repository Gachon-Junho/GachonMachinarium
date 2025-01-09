using System;
using UnityEngine;

public class HomeDropArea : MonoBehaviour
{
    [SerializeField]
    private ItemInfo itemInfo;

    private void OnTriggerEnter(Collider other)
    {
        var home = other.gameObject.GetComponent<Home>();

        if (home == null)
            return;

        Destroy(home.gameObject, 0.5f);
        // TODO: Home의 이미지 페이드아웃
        var item = itemInfo.CreateItem();
    }
}
