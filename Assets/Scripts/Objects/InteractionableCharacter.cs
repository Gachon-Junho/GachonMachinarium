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
    private Sprite[] dialogs;

    protected SpriteRenderer[] DialogElements => dialogElements ??= dialogContainer.GetComponentsInChildren<SpriteRenderer>();

    private SpriteRenderer[] dialogElements;

    private bool isInteracting => dialogs.Length > dialogIndex;
    private int dialogIndex;

    protected override void StartInteraction()
    {
        if (dialogIndex >= dialogs.Length)
        {
            StopAllCoroutines();
            this.FadeTo(0, 0.5f, Easing.Out);
            dialogIndex = 0;
            OnCompletedInteraction();
            return;
        }

        OnDialogAt(dialogIndex);
        dialogRenderer.sprite = dialogs[dialogIndex++];

        StopAllCoroutines();
        this.FadeToFromZero(1, 0.5f, Easing.Out);
    }

    // TODO: 자식클래스에서 재정의하여 대화위치에 따른 애니메이션 변화?
    protected abstract void OnDialogAt(int index);

    protected abstract void OnCompletedInteraction();
}
