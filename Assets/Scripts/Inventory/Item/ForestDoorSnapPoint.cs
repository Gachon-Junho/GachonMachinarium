using System.Collections;
using System.Linq;
using UnityEngine;

public class ForestDoorSnapPoint : ItemSnapPoint, IHasColor
{
    public Color Color
    {
        get => DialogElements.First().color;
        set => DialogElements.ForEach(s => s.color = value);
    }

    protected SpriteRenderer[] DialogElements => dialogElements ??= GetComponentsInChildren<SpriteRenderer>();

    private SpriteRenderer[] dialogElements;

    public override void OnItemSnapped(Item item)
    {
        this.FadeTo(0, 1, Easing.Out);
        item.FadeTo(0, 1, Easing.Out);

        Destroy(gameObject, 1);
        Destroy(item.gameObject, 1);
    }
}
