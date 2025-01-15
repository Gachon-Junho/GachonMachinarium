using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BartenderExitPoint : PlayerTouchPoint
    {
        [SerializeField]
        private Vector3 cameraPosition = new Vector3(-41.2f, 1.84f, -10);

        [SerializeField]
        private Vector3 playerPosition = new Vector3(-41.2f, 1.84f, 0);


        protected override void OnPlayerEntered()
        {
            base.OnPlayerEntered();

            SceneTransitionManager.Current.FadeInOutScreen(1, 1, Color.black);

            this.StartDelayedSchedule(() =>
            {
                Camera.main!.GetComponent<PlayerFollowingCamera>().Enabled = true;
                Camera.main!.transform.position = cameraPosition;
                Player.Current.transform.position = playerPosition;
            }, 1);
        }
    }
}
