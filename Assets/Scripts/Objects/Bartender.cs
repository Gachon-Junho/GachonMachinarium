﻿public class Bartender : InteractionableCharacter
{
    public static bool Completed;

    private bool rewarded;

    protected override void OnDialogAt(int index)
    {
        // TODO: 애니메이션 변화나 대화상자 말고 바꾸고 싶은 것들
    }

    protected override void OnCompletedInteraction()
    {
        // TODO: 미션을 띄워볼까요...
        if (Completed && !rewarded)
        {
            rewarded = true;

        }
    }
}
