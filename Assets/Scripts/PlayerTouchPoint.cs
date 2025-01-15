using System;
using UnityEngine;

public class PlayerTouchPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;


    }

    protected virtual void OnPlayerEntered()
    {

    }
}
