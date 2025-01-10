using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyPiece : AdjustableColor, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private new Collider2D collider;

    private Vector2 initialPosition;
    private RaycastHit hit;
    private bool hits;

    private CityPuzzle puzzle;

    public bool Snapped;

    private void Start()
    {
        initialPosition = transform.position;
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

        StopAllCoroutines();
        this.MoveTo(initialPosition, 0.5f, Easing.OutQuint);
        Snapped = false;
    }
}
