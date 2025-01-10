using System.Collections;
using UnityEngine;

public class ProxyMonoBehavior : Singleton<ProxyMonoBehavior>, IHasSample
{
    public AudioSource Source => source;
    public AudioClip Sample => sample;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip sample;

    public void Play()
    {
        Source.clip = Sample;
        Source.Play();
    }

    public void Play(AudioClip clip)
    {
        Source.clip = clip;
        Source.Play();
    }
}
