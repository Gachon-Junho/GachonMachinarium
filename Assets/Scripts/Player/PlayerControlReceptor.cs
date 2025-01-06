using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class PlayerControlReceptor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Player player;

    private bool isControlling;

    private void Update()
    {
        if (!isControlling)
            return;

        float destination = Camera.main!.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main!.transform.position.z)).x - Camera.main!.transform.position.x * 2;

        player.MoveTo(destination);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        var hits = Physics.RaycastAll(ray);
        var item = hits.FirstOrDefault(h => h.collider.CompareTag("Item"));

        if (item.collider != null)
        {
            player.OnItemClicked(item.collider.GetComponentInParent<Item>());
            return;
        }

        // if (Physics.Raycast(ray, out hit, float.MaxValue))
        // {
        //     if (hit.collider.CompareTag("Player"))
        //         return;
        //
        //     if (hit.collider.CompareTag("Item"))
        //     {
        //         player.OnItemClicked(hit.collider.GetComponentInParent<Item>());
        //     }
        // }

        isControlling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isControlling = false;
    }
}
