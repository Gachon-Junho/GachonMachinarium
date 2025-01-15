using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneTransitionManager : Singleton<SceneTransitionManager>, IHasColor
{
    public Color Color
    {
        get => Image.color;
        set
        {
            Image.color = value;
            ChildColors.ForEach(g => g.color = value);
        }
    }

    protected Image Image => image ??= panel.GetComponent<Image>();

    protected Graphic[] ChildColors => childColor ??= panel.GetComponentsInChildren<Graphic>();

    private Graphic[] childColor;


    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private float fadeInDuration = 1;

    [SerializeField]
    private float fadeOutDuration = 1;

    [SerializeField]
    private float timeUntilFadeOut = 0;

    [SerializeField]
    private bool showFadeIn;

    [SerializeField]
    private bool showFadeOut;

    [SerializeField]
    private Easing fadeInEasing = Easing.Out;

    [SerializeField]
    private Easing fadeOutEasing = Easing.Out;

    [SerializeField]
    private Color fadeColor;

    private Image image;

    private void Start()
    {
        if (!showFadeOut)
            return;

        panel.SetActive(true);
        this.StartDelayedSchedule(() => fadeOut(fadeOutDuration, fadeColor), timeUntilFadeOut);
    }

    public void SwitchScene(string sceneName)
    {
        if (!showFadeIn)
            return;

        fadeIn(fadeInDuration, fadeColor);
        this.StartDelayedSchedule(() => this.LoadSceneAsync(sceneName), fadeInDuration);
    }

    public void FadeIn(float duration, Action after = null)
    {
        var prepare = fadeColor;
        prepare.a = 0;

        Color = prepare;
        panel.SetActive(true);

        this.FadeTo(1, duration, fadeInEasing);
        this.StartDelayedSchedule(() => after?.Invoke(), 1);
    }

    private void fadeIn(float duration, Color color)
    {
        var prepare = color;
        prepare.a = 0;

        Color = prepare;
        panel.SetActive(true);

        this.FadeTo(1, duration, fadeInEasing);
    }

    private void fadeOut(float duration, Color color)
    {
        Color = color;
        panel.SetActive(true);

        this.FadeToFromOne(0, duration, fadeOutEasing);
        this.StartDelayedSchedule(() => panel.SetActive(false), fadeOutDuration);
    }

    public void FadeInOutScreen(float fadeDuration, float delayUntilFadeOut, Color color)
    {
        fadeIn(fadeDuration, color);

        this.StartDelayedSchedule(() => fadeOut(fadeDuration, color), fadeDuration + delayUntilFadeOut);
    }
}
