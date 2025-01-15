using UnityEngine;

public class BartenderExit : PlayerTouchPoint
{
    protected override void OnPlayerEntered()
    {
        base.OnPlayerEntered();

        SceneTransitionManager.Current.SwitchScene("GameplayScene");
    }
}
