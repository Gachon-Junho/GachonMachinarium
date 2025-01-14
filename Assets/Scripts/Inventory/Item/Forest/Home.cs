using System;
using UnityEngine;
using UnityEngine.XR;

public class Home : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private int countUntilDrop = 2;

    [SerializeField]
    private AudioClip fall;

    private ForestStoneItem previousItem;

    // TODO: 이미지 들어오면 IHasColor 추가
    private void OnCollisionEnter(Collision other)
    {
        var item = other.gameObject.GetComponent<ForestStoneItem>();

        if (item != null && previousItem != item)
        {
            if (--countUntilDrop <= 0)
            {
                // 2번 맞음
                ProxyMonoBehavior.Current.Play(fall);
                rigidbody.isKinematic = false;

                return;
            }

            // 2번 맞기 전
            previousItem = item;
        }
    }
}
