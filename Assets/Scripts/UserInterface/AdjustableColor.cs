using UnityEngine;
using UnityEngine.UI;

public class AdjustableColor : MonoBehaviour, IHasColor
{
    public Color Color
    {
        get => image.color;
        set => image.color = value;
    }

    // TODO: 노출시켜서 상속후 사용이 편리하게?
    [SerializeField]
    private Image image;
}
