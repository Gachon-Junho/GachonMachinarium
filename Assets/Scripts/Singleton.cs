using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour, ISingleton<T>
    where T : MonoBehaviour
{
    public static bool AllowMultipleInitialize = true;

    public static T Current
    {
        get => current;
        set
        {
            if (current == null || AllowMultipleInitialize)
            {
                current = value;
                return;
            }

            throw new InvalidOperationException($"{typeof(T)} is already Initialized. Consider set AllowMultipleInitialize to false.");
        }
    }

    private static T current;

    protected virtual void Awake()
    {
        Current = this as T;
    }
}