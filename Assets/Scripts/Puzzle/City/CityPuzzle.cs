using System.Linq;
using UnityEngine;

public class CityPuzzle : Puzzle
{
    [SerializeField]
    private KeyPieceSnapPoint[] snapPoints;

    [SerializeField]
    private KeyPiece[] keyPieces;

    public override void Initialize()
    {
        snapPoints.ForEach(s => s.Initialize(this));
        keyPieces.ForEach(k => k.ResetToInitialState(this));
    }

    protected override bool CheckCondition()
    {
        bool success = true;

        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (!ReferenceEquals(snapPoints[i].KeyPiece, keyPieces[i]))
                success = false;
        }

        if (!success && snapPoints.ToList().TrueForAll(s => s.KeyPiece != null))
        {
            UpdateState(PuzzlePlayingState.Failed);
            this.StartDelayedSchedule(() => UpdateState(PuzzlePlayingState.Playing), 2f);
        }

        return success;
    }

    public void Commit() => CommitChange();

    protected override void CommitChange()
    {
        base.CommitChange();
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();

        print("Success!");

        this.StartDelayedSchedule(() =>
        {
            gameObject.SetActive(false);
            PuzzlePanel.SetActive(false);
        }, 1);
    }

    protected override void OnFailed()
    {
        base.OnFailed();

        print("Failed!");
        this.StartDelayedSchedule(() =>
        {
            Initialize();
            UpdateState(PuzzlePlayingState.Playing);
        }, 2f);
    }

    public KeyPieceSnapPoint GetHovering()
    {
        return snapPoints.FirstOrDefault(p => p.Hovering);
    }
}
