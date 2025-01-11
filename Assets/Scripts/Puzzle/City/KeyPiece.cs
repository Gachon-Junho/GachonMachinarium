using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyPiece : AdjustableColor, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private new Collider2D collider;

    private Vector2? initialPosition;
    private RaycastHit hit;
    private bool hits;

    private CityPuzzle puzzle;

    public bool Snapped;

    private void Start()
    {
        initialPosition = transform.position;

        // Rect Transform과 Transform의 관련성이 Start() 호출 이전에 어떻게 되는지 모르겠음.
        // 이상하지만 다시 한 번 위치를 설정합니다.
        StopAllCoroutines();
        this.MoveTo(initialPosition!.Value);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Snapped)
            return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Snapped)
            return;

        var snap = puzzle.GetHovering();

        if (snap == null || snap.KeyPiece != null)
        {
            ResetToInitialState(null);
            return;
        }

        snap.Snap(this);
        this.MoveTo(snap.transform.position, 0.5f, Easing.OutQuint);
        Snapped = true;
    }

    public void ResetToInitialState(CityPuzzle puzzle)
    {
        this.puzzle ??= puzzle;

        // Start 이전에 Initialize()가 실행되면 null일 가능성이 있습니다.
        initialPosition ??= transform.position;

        StopAllCoroutines();
        this.MoveTo(initialPosition.Value, 0.5f, Easing.OutQuint);
        Snapped = false;
    }
}
