using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private int countUntilDrop = 2;

    // TODO: 이미지 들어오면 IHasColor 추가
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ForestStoneItem>() != null)
        {
            if (--countUntilDrop <= 0)
            {
                // 2번 맞음
                rigidbody.isKinematic = false;

                return;
            }

            // 2번 맞기 전
        }
    }
}
