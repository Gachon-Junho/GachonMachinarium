using UnityEngine;

public class PlayerFollowingCamera : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private Player player;

    [SerializeField]
    private float moveSpeed;

    public bool Enabled = true;

    private bool isRequiredCatchUp;

    // Update is called once per frame
    void Update()
    {
        float vPos = camera.WorldToViewportPoint(player.transform.position).x;

        if (vPos >= 0.55f || vPos <= 0.45f)
            isRequiredCatchUp = true;

        if (Enabled && isRequiredCatchUp)
        {
            camera.transform.position = Vector3.Lerp(transform.position,
                            new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z),
                            Time.deltaTime * moveSpeed);
        }

        if (Precision.AlmostEquals(player.transform.position.x, camera.transform.position.x))
            isRequiredCatchUp = false;

    }
}
