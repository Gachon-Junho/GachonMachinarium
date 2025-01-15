using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour, IHasColor
{
    public Color Color
    {
        get => image.color;
        set => image.color = value;
    }

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private RawImage image;

    private static bool played;

    private void Start()
    {
        if (played)
            return;

        this.ColorTo(Color.white, 0.1f, Easing.Out);

        videoPlayer.Play();
        played = true;

        this.StartDelayedSchedule(() =>
        {
            this.ColorTo(Color.clear, 1, Easing.Out);
            this.StartDelayedSchedule(() =>
            {
                gameObject.SetActive(false);
            }, 1);
        }, (float)videoPlayer.clip.length);
    }
}
