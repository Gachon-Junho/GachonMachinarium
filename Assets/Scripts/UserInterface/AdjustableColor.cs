using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AdjustableColor : MonoBehaviour, IHasColor
{
    public Color Color
    {
        get => Image.color;
        set => Image.color = value;
    }

    [FormerlySerializedAs("image")]
    [SerializeField]
    public Image Image;
}
