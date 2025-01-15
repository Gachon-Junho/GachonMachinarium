using System;
using UnityEngine;
using UnityEngine.Video;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private static bool clicked;

    private void OnMouseDown()
    {
        if (clicked)
            return;

        clicked = true;
        EndingPlayer.Current.MoveTo(transform.position.z);

        this.StartDelayedSchedule(() =>
        {
            SceneTransitionManager.Current.FadeIn(2f, () =>
            {
                videoPlayer.gameObject.SetActive(true);
                videoPlayer.Play();
            });
        }, 1);
    }
}
