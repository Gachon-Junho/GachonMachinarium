using System;
using UnityEngine;

public class FallenTreeBranch : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private Vector3 playerPosition;

    private void OnCollisionEnter(Collision other)
    {
        var item = other.gameObject.GetComponent<StoneGrassItem>();

        if (item == null)
            return;

        item.Rigidbody.isKinematic = true;
        SceneTransitionManager.Current.FadeInOutScreen(1f, 1f, Color.black);
        this.StartDelayedSchedule(() =>
        {
            Player.Current.transform.position = playerPosition;
            Destroy(gameObject);
        }, 1f);
    }
}
