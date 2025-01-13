using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private Collider boundary;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip pickitem;

    private float? requestedPosition;
    private int lastDirection;

    private bool flipped;
    private bool isRotating;

    private Coroutine prevTransform;
    private List<Item> inBoundary = new List<Item>();

    private static readonly int expanded = Animator.StringToHash("Expanded");
    private static readonly int is_walking = Animator.StringToHash("IsWalking");

    private bool formChanging;

    private void Update()
    {
        if (!requestedPosition.HasValue || formChanging)
            return;

        if (!Precision.AlmostEquals(-requestedPosition.Value, transform.position.x))
        {
            if (!animator.GetBool(is_walking))
                animator.SetBool(is_walking, true);

            // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
            int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

            transform.Translate(new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0));
        }
        else
        {
            requestedPosition = null;
            OnMovedToDestination();
        }
    }

    public void MoveTo(float x)
    {
        // 폰 전환 중에는 이동 차단
        if (formChanging)
            return;

        requestedPosition = x;

        int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

        if (lastDirection != direction)
        {
            flipped = direction == -1;

            // Shit Code
            if (prevTransform != null)
                ProxyMonoBehavior.Current.StopAllCoroutines();

            prevTransform = sprite.transform.RotateTo(new Vector3(0, flipped ? -180 : 0, 0), 0.5f, Easing.OutQuint);
        }

        lastDirection = direction;
    }

    public void OnItemClicked(Item item)
    {
        if (!inBoundary.Exists(i => ReferenceEquals(item, i)))
            return;

        var result = Inventory.Current.Add(item.Info);

        if (result == null)
            return;

        item.MoveTo(transform.position, 0.12f, Easing.OutQuint);
        item.FadeTo(0, 0.12f, Easing.OutQuint);
        inBoundary.Remove(item);
        Destroy(item.gameObject, 0.12f);

        ProxyMonoBehavior.Current.Play(pickitem);
    }

    public void SwitchForm()
    {
        animator.SetBool(expanded, !animator.GetBool(expanded));
        formChanging = true;

        // 애니메이션 전환 완료까지 대기
        this.StartDelayedSchedule(() => formChanging = false, 0.5f);
    }

    private void OnMovedToDestination()
    {
        // TODO: 그냥그냥
        animator.SetBool(is_walking, false);
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
