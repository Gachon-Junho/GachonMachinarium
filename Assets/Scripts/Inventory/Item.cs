using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo Info;
    
    private bool dragging;
    private RaycastHit hit;

    private void Update()
    {
    }

    public bool OnItemDropped()
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            if (!hit.collider.CompareTag("SnapPoint"))
                return false;
            
            this.MoveTo(hit.collider.transform.position, 1f, Easing.OutQuint);

            return true;
        }
        
        return false;
    }

    private IEnumerator snap()
    {
        yield break;
    }
}