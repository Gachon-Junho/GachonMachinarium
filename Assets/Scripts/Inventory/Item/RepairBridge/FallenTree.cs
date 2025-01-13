using UnityEngine;

public class FallenTree : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private ItemInfo rewardItem;

    [SerializeField]
    private Vector3 rewardItemPosition;

    private AudioClip hitTree;

    private AudioClip brokeAxe;

    // TODO: 이미지 들어오면 IHasColor 추가
    private void OnCollisionEnter(Collision other)
    {
        var item = other.gameObject.GetComponent<AxeItem>();

        if (item == null)
            return;

        // 여기서부터 도끼가 나무에 맞음.
        item.Rigidbody.isKinematic = true;

        // 1초동안 화면이 페이드 인 되고 1.5초 동안 상태 유지후 1초동안 페이드 아웃
        SceneTransitionManager.Current.FadeInOutScreen(1f, 1.5f, Color.black);

        // ProxyMonoBehavior.Current.Play(hitTree);

        // 맞고나서 1초 뒤에 아래 코드 실행
        this.StartDelayedSchedule(() =>
        {
            Destroy(gameObject);
            Destroy(item.gameObject);

            // ProxyMonoBehavior.Current.Play(brokeAxe);

            var reward = rewardItem.CreateItem();
            reward.transform.position = rewardItemPosition;
        }, 1f);
    }
}
