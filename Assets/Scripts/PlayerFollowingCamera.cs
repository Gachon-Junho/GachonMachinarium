using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingCamera : MonoBehaviour
{
    [SerializeField] 
    private Camera camera;
    
    [SerializeField] 
    private Player player;

    [SerializeField] 
    private float moveSpeed;

    public bool Enabled = true;

    private bool isRequiredCatchUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vPos = camera.WorldToViewportPoint(player.transform.position).x;

        if (vPos >= 0.75f || vPos <= 0.25f)
            isRequiredCatchUp = true;

        if (Enabled && isRequiredCatchUp)
        {
            camera.transform.position = Vector3.Lerp(transform.position,
                            new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z),
                            0.001f);
        }

        if (Precision.AlmostEquals(player.transform.position.x, camera.transform.position.x))
            isRequiredCatchUp = false;

    }
}
