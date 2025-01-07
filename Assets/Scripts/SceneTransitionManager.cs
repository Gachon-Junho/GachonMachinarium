using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : Singleton<SceneTransitionManager>, IHasColor
{
    public Color Color
    {
        get => Image.color;
        set => Image.color = value;
    }

    protected Image Image => image ??= panel.GetComponent<Image>();

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private float fadeInDuration = 1;

    [SerializeField]
    private float fadeOutDuration = 1;

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

        Color = fadeColor;
        panel.SetActive(true);

        this.FadeTo(0, fadeOutDuration, fadeOutEasing);
        this.StartDelayedSchedule(() => panel.SetActive(false), fadeOutDuration);
    }

    public void SwitchScene(string sceneName)
    {
        if (!showFadeIn)
            return;

        var prepare = fadeColor;
        prepare.a = 0;

        Color = prepare;
        panel.SetActive(true);

        this.FadeTo(1, fadeInDuration, fadeInEasing);
        this.StartDelayedSchedule(() => this.LoadSceneAsync(sceneName), fadeInDuration);
    }
}
