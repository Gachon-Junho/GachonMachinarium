using UnityEngine;

public class ShopBoxKeyPuzzle : Puzzle
{
    [SerializeField]
    private Collider[] snapPoints;

    [SerializeField]
    private KeyPiece[] keyPieces;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    protected override bool CheckCondition()
    {
        throw new System.NotImplementedException();
    }

    protected override void CommitChange()
    {
        base.CommitChange();
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
    }

    protected override void OnFailed()
    {
        base.OnFailed();
    }


}
