using UnityEngine;

public class ForestStoneItem : Item
{
    [SerializeField]
    private new Rigidbody rigidbody;

    protected override void OnItemDropped(RaycastHit hitted)
    {
        Collider.isTrigger = false;
        rigidbody.
        GetComponent<Rigidbody>().AddForce(hitted.transform.position - transform.position);
    }
}
