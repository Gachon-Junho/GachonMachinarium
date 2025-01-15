using System.Collections;
using UnityEngine;

public class EndingPoint : PlayerTouchPoint
{
    protected override void OnPlayerEntered()
    {
        base.OnPlayerEntered();

        SceneTransitionManager.Current.SwitchScene("EndingScene");
    }
}
