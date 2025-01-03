using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Info", menuName = "Item/Item Info")]
public class ItemInfo : ScriptableObject, IEquatable<ItemInfo>
{
    public string Name;
    public ItemType Type;

    public Sprite ItemIcon;
    public GameObject ItemPrefab;
    public GameObject ItemViewPrefab;

    public ItemView CreateItemView()
    {
        var view = Instantiate(ItemViewPrefab).GetComponent<ItemView>();
        view.name = Name;
        view.ItemIcon = ItemIcon;
        view.ItemInfo = this;

        return view;
    }

    public Item CreateItem()
    {
        var item = Instantiate(ItemPrefab).GetComponent<Item>();
        item.name = Name;
        item.Info = this;

        return item;
    }

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
    Puzzle,
    CombinationOnly
}