using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] 
    private GameObject content;

    [SerializeField] 
    private GameObject itemView;
    
    private List<ItemView> items = new List<ItemView>();

    [SerializeField] 
    private int maxCapacity = 9;

    public void Add(Item item)
    {
        if (items.Count >= maxCapacity)
            return;

        Instantiate(itemView, content.transform);
        itemView.GetComponent<ItemView>();
    }
}