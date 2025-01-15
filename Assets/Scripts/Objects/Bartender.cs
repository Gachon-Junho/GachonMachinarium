using System;
using UnityEngine;

public class Bartender : InteractionableCharacter
{
    public static bool IsReadyToStartPuzzle;
    public static bool Completed;

    private bool rewarded;

    [SerializeField]
    private ItemInfo rewardItem;

    protected override void OnDialogAt(int index)
    {
        // TODO: 애니메이션 변화나 대화상자 말고 바꾸고 싶은 것들
    }

    protected override void OnCompletedInteraction()
    {
        // TODO: 미션을 띄워볼까요...
        if (rewarded)
            return;

        if (Completed && IsReadyToStartPuzzle)
        {
            SetComplete();
            Inventory.Current.Add(rewardItem);
            rewarded = true;
        }
    }

    public void SetComplete()
    {
        Completed = true;
        Dialogs = Array.Empty<Sprite>();
    }
}
