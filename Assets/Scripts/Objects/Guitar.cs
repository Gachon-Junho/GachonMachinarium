using System;
using System.Collections;
using UnityEngine;

public class Guitar : InteractionableCharacter
{
    public static bool Completed;

    private bool rewarded;

    [SerializeField]
    private GameObject puzzlePanel;

    [SerializeField]
    private GuitarBoxHPuzzle puzzle;

    [SerializeField]
    private ItemInfo targetItem;

    [SerializeField]
    private ItemInfo targetItem2;

    [SerializeField]
    private ItemInfo targetItem3;

    [SerializeField]
    private ItemInfo guitar;

    [SerializeField]
    private InteractionableCharacter[] Band;

    [SerializeField]
    private InteractionableCharacter[] BartenderBand;

    private void Update()
    {
       BlockInteraction = (!Inventory.Current.Exists(targetItem) || !Inventory.Current.Exists(targetItem2) || !Inventory.Current.Exists(targetItem3)) || rewarded;
    }

    protected override void OnDialogAt(int index)
    {
        // TODO: 애니메이션 변화나 대화상자 말고 바꾸고 싶은 것들
    }

    protected override bool OnStartInteraction()
    {
        if (Completed)
        {
            bool success = Inventory.Current.Remove(guitar);

            if (!success)
                return false;

            SetComplete();

            this.StartDelayedSchedule(() =>
            {
                Band.ForEach(b => b.FadeCharacter(0, 1, Easing.Out));
                this.StartDelayedSchedule(() =>
                {
                    Band.ForEach(b => b.gameObject.SetActive(false));
                }, 1);

                BartenderBand.ForEach(b => b.gameObject.SetActive(true));
            }, 1.5f);

            Bartender.Completed = true;
            Bartender.IsReadyToStartPuzzle = true;

            rewarded = true;
        }

        return !rewarded;
    }

    protected override void OnCompletedInteraction()
    {
        // TODO: 미션을 띄워볼까요...
        if (!Completed)
        {
            puzzlePanel.SetActive(true);
            puzzle.gameObject.SetActive(true);
        }
    }

    public void SetComplete()
    {
        Completed = true;
        Dialogs = Array.Empty<Sprite>();
        Animator.SetBool("Completed", true);
    }
}
