using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private Collider boundary;

    private float? requestedPosition;
    private float lastDirection;

    private bool flipped;
    private bool isRotating;

    private Coroutine prevTransform;
    private List<Item> inBoundary = new List<Item>();

    private void Update()
    {
        if (!requestedPosition.HasValue)
            return;

        if (!Precision.AlmostEquals(-requestedPosition.Value, transform.position.x))
        {
            // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
            int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

            transform.Translate(new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0));
            lastDirection = direction;
        }
        else
        {
            requestedPosition = null;
        }
    }

    public void MoveTo(float x)
    {
        requestedPosition = x;

        int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

        if (!Precision.AlmostEquals(lastDirection, direction))
        {
            flipped = Precision.AlmostEquals(direction, -1);

            // Shit Code
            if (prevTransform != null)
                ProxyMonoBehavior.Current.StopCoroutine(prevTransform);

            prevTransform = sprite.transform.RotateTo(new Vector3(0, flipped ? -180 : 0, 0), 0.5f, Easing.OutQuint);
        }

        lastDirection = direction;
    }

    public void OnItemClicked(Item item)
    {
        if (!inBoundary.Exists(i => ReferenceEquals(item, i)))
            return;

        // TODO: 인벤토리에 추가 후 삭제
        var result = Inventory.Current.Add(item.Info);

        if (result == null)
            return;

        item.MoveTo(transform.position, 0.12f, Easing.OutQuint);
        item.FadeTo(0, 0.12f, Easing.OutQuint);
        inBoundary.Remove(item);
        Destroy(item.gameObject, 0.12f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Item"))
            return;

        var item = other.gameObject.GetComponentInParent<Item>();

        if (!inBoundary.Exists(i => ReferenceEquals(item, i)))
            inBoundary.Add(item);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Item"))
            return;

        var item = other.gameObject.GetComponentInParent<Item>();

        if (inBoundary.Exists(i => ReferenceEquals(item, i)))
            inBoundary.Remove(item);
    }
}
