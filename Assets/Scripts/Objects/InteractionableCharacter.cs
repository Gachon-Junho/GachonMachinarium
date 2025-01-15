using System.Collections;
using UnityEngine;

public abstract class InteractionableCharacter : InteractionableObject, IHasColor
{
    public Color Color
    {
        get => dialogRenderer.color;
        set
        {
            dialogRenderer.color = value;
            DialogElements.ForEach(s => s.color = value);
        }
    }

    [SerializeField]
    private GameObject dialogContainer;

    [SerializeField]
    private SpriteRenderer dialogRenderer;

    [SerializeField]
    protected Animator Animator;

    [SerializeField]
    protected Sprite[] Dialogs;

    protected SpriteRenderer[] DialogElements => dialogElements ??= dialogContainer.GetComponentsInChildren<SpriteRenderer>();

    private SpriteRenderer[] dialogElements;

    private bool isInteracting => Dialogs.Length > dialogIndex;
    private int dialogIndex;

    public bool BlockInteraction;

    protected override void StartInteraction()
    {
        bool start = OnStartInteraction();

        if (Dialogs.Length == 0 || BlockInteraction || !start)
            return;

        if (dialogIndex >= Dialogs.Length)
        {
            StopAllCoroutines();
            this.FadeTo(0, 0.5f, Easing.Out);
            dialogIndex = 0;
            OnCompletedInteraction();
            return;
        }

        OnDialogAt(dialogIndex);
        dialogRenderer.sprite = Dialogs[dialogIndex++];

        StopAllCoroutines();
        this.FadeToFromZero(1, 0.5f, Easing.Out);
    }

    // TODO: 자식클래스에서 재정의하여 대화위치에 따른 애니메이션 변화?
    protected abstract void OnDialogAt(int index);

    protected virtual bool OnStartInteraction()
    {
        return true;
    }

    protected abstract void OnCompletedInteraction();

    public void FadeCharacter(float alpha, double duration = 0, Easing easing = Easing.None)
    {
        var renderer = GetComponent<SpriteRenderer>();
        var cor = StartCoroutine(transformLoop(alpha, Time.time, Time.time + duration));

        IEnumerator transformLoop(float to, double startTime, double endTime)
        {
            float start = renderer.color.a;

            while (Time.time < endTime)
            {
                renderer.color = new Color(renderer.color.r,
                    renderer.color.g,
                    renderer.color.b,
                    Interpolation.ValueAt(Time.time, start, to, startTime, endTime, new EasingFunction(easing)));

                yield return null;
            }
        }
    }
}
