using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BartenderDoor : InteractionableObject
    {
        [SerializeField]
        private Vector3 cameraPosition;

        [SerializeField]
        private Vector3 playerPosition;

        protected override void StartInteraction()
        {
            SceneTransitionManager.Current.FadeInOutScreen(1, 1, Color.black);

            this.StartDelayedSchedule(() =>
            {
                Camera.main!.GetComponent<PlayerFollowingCamera>().Enabled = false;
                Camera.main!.transform.position = cameraPosition;
                Player.Current.transform.position = playerPosition;
            }, 1);
        }
    }
}
