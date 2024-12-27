using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Info", menuName = "Item")]
public class ItemInfo : ScriptableObject, IEquatable<ItemInfo>
{
    public string Name;
    public ItemType Type;

    public Sprite ItemIcon;
    public GameObject ItemPrefab;

    public bool Equals(ItemInfo i)
    {
        if (i == null)
            return false;
        
        return i.Name == Name &&
               i.Type == Type &&
               i.ItemIcon == ItemIcon &&
               i.ItemPrefab == ItemPrefab;
    }
}

public enum ItemType
{
    Platform,
    Puzzle
}