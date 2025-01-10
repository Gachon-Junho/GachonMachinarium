using UnityEngine;

public interface IHasSample
{
    AudioSource Source { get; }
    AudioClip Sample { get; }
    void Play();
}
