using UnityEngine;

public interface IHasSample
{
    AudioClip Sample { get; }
    void Play();
}
