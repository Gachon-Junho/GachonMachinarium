using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class MainMenuBackground : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioClip start;

    public const string GAMEPLAY_SCENE = @"GameplayScene";

    private bool clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        ProxyMonoBehavior.Current.Play(start);

        if (clicked)
            return;

        clicked = true;
        SceneTransitionManager.Current.SwitchScene(GAMEPLAY_SCENE);
    }
}
