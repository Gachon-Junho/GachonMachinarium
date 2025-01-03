using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject sprite;

    private float? requestedPosition;
    private float lastDirection;

    private bool flipped;
    private bool isRotating;

    private Coroutine prevTransform;

    private void Update()
    {
        if (!requestedPosition.HasValue)
            return;

        if (!Precision.AlmostEquals(-requestedPosition.Value, transform.position.x))
        {
            // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
            int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

            transform.Translate(new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0));
            lastDirection = direction;
        }
        else
        {
            requestedPosition = null;
        }
    }

    public void MoveTo(float x)
    {
        requestedPosition = x;

        int direction = -requestedPosition.Value >= transform.position.x ? 1 : -1;

        if (!Precision.AlmostEquals(lastDirection, direction))
        {
            flipped = Precision.AlmostEquals(direction, -1);

            // Shit Code
            if (prevTransform != null)
                ProxyMonoBehavior.Current.StopCoroutine(prevTransform);

            prevTransform = sprite.transform.RotateTo(new Vector3(0, flipped ? -180 : 0, 0), 0.5f, Easing.OutQuint);
        }

        lastDirection = direction;
    }
}
