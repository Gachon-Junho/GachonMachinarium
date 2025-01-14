using UnityEngine;

public class FallenTree : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private ItemInfo rewardItem;

    [SerializeField]
    private Vector3 rewardItemPosition;

    [SerializeField]
    private Vector3 playerPosition;

    [SerializeField]
    private AudioClip hitTree;

    [SerializeField]
    private AudioClip brokeAxe;

    // TODO: 이미지 들어오면 IHasColor 추가
    private void OnCollisionEnter(Collision other)
    {
        var item = other.gameObject.GetComponent<Item>();

        if (item == null)
            return;

        switch (item)
        {
            case StoneGrassItem s:
                s.Rigidbody.isKinematic = true;
                SceneTransitionManager.Current.FadeInOutScreen(1f, 1f, Color.black);

                this.StartDelayedSchedule(() =>
                {
                    s.Hitted.collider.isTrigger = false;
                    Player.Current.transform.position = playerPosition;
                }, 1f);

                break;

            case AxeItem a:
                a.Rigidbody.isKinematic = true;

                // 1초동안 화면이 페이드 인 되고 1.5초 동안 상태 유지후 1초동안 페이드 아웃
                SceneTransitionManager.Current.FadeInOutScreen(1f, 1f, Color.black);

                ProxyMonoBehavior.Current.Play(hitTree);

                // 맞고나서 1초 뒤에 아래 코드 실행
                this.StartDelayedSchedule(() =>
                {
                    var reward = rewardItem.CreateItem();
                    reward.transform.position = rewardItemPosition;

                    ProxyMonoBehavior.Current.Play(brokeAxe);

                    Destroy(FindFirstObjectByType<StoneGrassItem>()?.gameObject);
                    Destroy(item.gameObject);
                    Destroy(gameObject);
                }, 1f);
                break;
        }
    }
}
