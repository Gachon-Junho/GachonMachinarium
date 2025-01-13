using UnityEngine;

public class AxeItem : Item
{
    public Rigidbody Rigidbody => rigidbody;

    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private float velocity = 5;

    protected override void OnItemDropped(RaycastHit snapPoint)
    {
        Collider.isTrigger = false;
        rigidbody.isKinematic = false;
        transform.position = Player.Current.transform.position + new Vector3(0, 0, -1);

        var direction = (snapPoint.transform.position - Player.Current.transform.position).normalized;

        rigidbody.AddForce(direction * velocity + new Vector3(1, 5f, 0), ForceMode.Impulse);
        snapPoint.collider.isTrigger = true;
    }
}