using System;
using UnityEngine;

public class PlayerTouchPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        OnPlayerEntered();
    }

    protected virtual void OnPlayerEntered()
    {

    }
}
