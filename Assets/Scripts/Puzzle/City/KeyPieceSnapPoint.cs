using UnityEngine;

public class KeyPieceSnapPoint : MonoBehaviour
{
    public KeyPiece KeyPiece { get; private set; }

    private CityPuzzle puzzle;

    public void Snap(KeyPiece piece)
    {
        KeyPiece = piece;
        puzzle.Commit();
    }

    public void Initialize(CityPuzzle puzzle)
    {
        this.puzzle = puzzle;
    }
}
