using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuBackground : MonoBehaviour, IPointerClickHandler
{
    public const string GAMEPLAY_SCENE = @"GameplayScene";

    private bool clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clicked)
            return;

        clicked = true;
        SceneTransitionManager.Current.SwitchScene(GAMEPLAY_SCENE);
    }
}
