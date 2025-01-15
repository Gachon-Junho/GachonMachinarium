using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayer : Singleton<EndingPlayer>
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float moveSpeed;

    private float? requestedPosition;
    private static readonly int is_walking = Animator.StringToHash("IsWalking");

    private int? direction;

    private void Update()
    {
        if (!requestedPosition.HasValue)
            return;

        if (!Precision.AlmostEquals(requestedPosition.Value, transform.position.z, Time.deltaTime))
        {
            if (!animator.GetBool(is_walking))
                animator.SetBool(is_walking, true);

            // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
            direction ??= -requestedPosition.Value >= transform.position.z ? 1 : -1;

            transform.Translate(new Vector3(0, 0, moveSpeed * direction!.Value * Time.deltaTime));
        }
        else
        {
            requestedPosition = null;
            animator.SetBool(is_walking, false);
        }
    }

    public void MoveTo(float z)
    {
        requestedPosition = z;
    }
}
