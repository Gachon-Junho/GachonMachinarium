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
        transform.position = eventData.position;

        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        hit = Physics.RaycastAll(ray, float.MaxValue).FirstOrDefault(h => h.collider.GetComponent<KeyPieceSnapPoint>());
        hits = hit.collider != null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!hits)
        {
            ResetToInitialState();
            return;
        }

        print(hit.collider.name);
        var snap = hit.collider.GetComponent<KeyPieceSnapPoint>();

        if (snap == null)
        {
            ResetToInitialState();
            return;
        }

        snap.Snap(this);
        this.MoveTo(snap.transform.position, 0.5f, Easing.OutQuint);
        Snapped = true;
    }

    public void ResetToInitialState()
    {
        StopAllCoroutines();
        this.MoveTo(initialPosition, 0.5f, Easing.OutQuint);
        Snapped = false;
    }
}
