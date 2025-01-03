using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCombinationTable", menuName = "Item/Combination Table")]
public class ItemCombinationTable : ScriptableObject
{
    public List<ItemCombinationBlueprint> Table;

    [Serializable]
    public class ItemCombinationBlueprint
    {
        public ItemInfo First;
        public ItemInfo Second;
        public ItemInfo Result;
    }
}