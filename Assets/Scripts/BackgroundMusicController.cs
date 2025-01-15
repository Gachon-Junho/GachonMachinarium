using UnityEngine;

public class BackgroundMusicController : Singleton<BackgroundMusicController>, IHasAudio
{
    public AudioSource AudioSource
    {
        get => audioSource;
        set => audioSource = value;
    }

    [SerializeField]
    private AudioSource audioSource;

    public void PlayMusic(AudioClip track, ulong delay)
    {
        audioSource.clip = track;
        audioSource.volume = 0;
        audioSource.Play(delay);
    }

    public void SwitchMusicGracefully(AudioClip track)
    {
        StopAllCoroutines();

        this.VolumeTo(0, 1, Easing.Out);
        this.StartDelayedSchedule(() =>
        {
            audioSource.Stop();
            audioSource.clip = track;
            audioSource.Play();

            this.VolumeTo(1, 1, Easing.Out);
        }, 1);
    }
}
