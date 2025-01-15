using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class PlayerControlReceptor : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    private bool isControlling;

    private void Update()
    {
        if (!isControlling)
            return;

        // float destination = Camera.main!.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main!.transform.position.z)).x - Camera.main!.transform.position.x * 2;

        // Player.Current.MoveTo(destination);
    }

    private void moveToPointerDirection()
    {
        float destination = Camera.main!.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main!.transform.position.z)).x - Camera.main!.transform.position.x * 2;

        Player.Current.MoveTo(destination);
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveToPointerDirection();

        isControlling = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isControlling = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);

        var player = hits.FirstOrDefault(h => h.collider.CompareTag("Player"));
        var item = hits.FirstOrDefault(h => h.collider.CompareTag("Item"));

        // 상호작용 가능한 요소에 클릭했을땐 플레이어와 연관된 입력 차단
        if (hits.FirstOrDefault(h => h.collider.CompareTag("Interactionable")).collider != null && item.collider == null && player.collider == null)
            return;

        // 플레이어 클릭
        if (player.collider != null)
        {
            Player.Current.SwitchForm();
        }

        // 아이템 클릭
        if (item.collider != null)
        {
            Player.Current.OnItemClicked(item.collider.GetComponentInParent<Item>());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);

        var interactionable = hits.FirstOrDefault(h => h.collider.CompareTag("Interactionable"));
        var player = hits.FirstOrDefault(h => h.collider.CompareTag("Player"));
        var item = hits.FirstOrDefault(h => h.collider.CompareTag("Item"));

        bool itemIn = item.collider != null && Player.Current.CheckItemInBoundary(item.collider.GetComponent<Item>());

        if (interactionable.collider == null && player.collider == null && !itemIn)
            moveToPointerDirection();
    }
}
