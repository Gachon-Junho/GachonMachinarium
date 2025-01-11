using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPieceSnapPoint : AdjustableColor, IPointerEnterHandler, IPointerExitHandler
{
    public KeyPiece KeyPiece { get; private set; }

    private CityPuzzle puzzle;
    private Color? initialColor;

    public bool Hovering { get; private set; }

    private void Start()
    {
        initialColor = Color;
    }

    public void Snap(KeyPiece piece)
    {
        KeyPiece = piece;
        puzzle.Commit();

        StopAllCoroutines();
        this.ColorTo(Color.clear, 0.3f, Easing.OutQuint);
    }

    public void Initialize(CityPuzzle puzzle)
    {
        this.puzzle = puzzle;
        KeyPiece = null;
        initialColor ??= Color;

        this.ColorTo(initialColor.Value, 0.3f, Easing.OutQuint);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hovering = true;
        StopAllCoroutines();
        this.ColorTo(KeyPiece == null ? Color.cyan : Color.red, 0.3f, Easing.OutQuint);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hovering = false;
        StopAllCoroutines();
        this.ColorTo(KeyPiece == null ? initialColor!.Value : Color.clear, 0.3f, Easing.OutQuint);
    }
}
