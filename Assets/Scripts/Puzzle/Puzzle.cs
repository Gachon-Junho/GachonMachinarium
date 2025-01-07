using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public PuzzlePlayingState State { get; private set; }

    /// <summary>
    /// 플레이 가능한 초기 상태로 초기화 합니다.
    /// </summary>
    public abstract void Initialize();

    public void Start()
    {
        Initialize();
    }

    public virtual void StartPlay()
    {
        State = PuzzlePlayingState.Playing;
    }

    protected virtual void FixedUpdate()
    {
        bool success = CheckCondition();

        if (success && State != PuzzlePlayingState.Completed)
        {
            State = PuzzlePlayingState.Completed;
            OnCompleted();
        }
    }

    /// <summary>
    /// 변경사항을 적용합니다.
    /// </summary>
    protected virtual void CommitChange()
    {
        CheckCondition();
    }

    /// <summary>
    /// 현재 퍼즐 상태를 업데이트 하면서 관련된 메서드를 실행합니다.
    /// </summary>
    /// <param name="newState">새로 적용할 상태.</param>
    protected void UpdateState(PuzzlePlayingState newState)
    {
        switch (newState)
        {
            case PuzzlePlayingState.NotPlaying:
                Initialize();
                State = newState;

                break;

            case PuzzlePlayingState.Playing:
                StartPlay();

                break;

            case PuzzlePlayingState.Failed:
                OnFailed();
                State = newState;

                break;

            case PuzzlePlayingState.Completed:
                OnCompleted();
                State = newState;

                break;
        }
    }

    /// <summary>
    /// 퍼즐이 조건에 만족하는지 확인합니다. <see cref="CommitChange"/>을 호출하고나서 호출됩니다.
    /// </summary>
    /// <returns>조건이 완전히 만족하는지의 여부.</returns>
    protected abstract bool CheckCondition();

    /// <summary>
    /// 퍼즐을 해결하는데 실패했을 때 호출됩니다.
    /// </summary>
    protected virtual void OnFailed()
    {
    }

    /// <summary>
    /// 퍼즐이 완료되었을 때 호출됩니다.
    /// </summary>
    protected virtual void OnCompleted()
    {
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
