using UnityEngine;

public interface ISingleton<T> where T : MonoBehaviour
{
    static T Current { get; set; }
}