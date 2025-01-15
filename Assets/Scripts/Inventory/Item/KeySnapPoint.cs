using System.Collections;
using UnityEngine;

public class KeySnapPoint : ItemSnapPoint
{
    [SerializeField]
    private GameObject puzzlePanel;

    [SerializeField]
    private CityPuzzle puzzle;

    public override void OnItemSnapped(Item item)
    {
        puzzlePanel.SetActive(true);
        puzzle.gameObject.SetActive(true);

        Destroy(item.gameObject);
        Destroy(gameObject);
    }
}
