using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class PlayerControlReceptor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isControlling;

    private void Update()
    {
        if (!isControlling)
            return;

        float destination = Camera.main!.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main!.transform.position.z)).x - Camera.main!.transform.position.x * 2;

        Player.Current.MoveTo(destination);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        var hits = Physics.RaycastAll(ray);
        var item = hits.FirstOrDefault(h => h.collider.CompareTag("Item"));
        var player = hits.FirstOrDefault(h => h.collider.CompareTag("Player"));

        if (player.collider != null)
        {
            Player.Current.SwitchForm();
            return;
        }

        if (item.collider != null)
        {
            Player.Current.OnItemClicked(item.collider.GetComponentInParent<Item>());
            return;
        }

        if (hits.FirstOrDefault(h => h.collider.CompareTag("Interactionable")).collider != null)
            return;

        isControlling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isControlling = false;
    }
}
