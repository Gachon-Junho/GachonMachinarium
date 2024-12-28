using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] 
    private GameObject content;

    [SerializeField] 
    private GameObject itemView;
    
    private List<ItemView> items = new List<ItemView>();

    [SerializeField] 
    private int maxCapacity = 9;

    private void Start()
    {
        items = content.GetComponentsInChildren<ItemView>().ToList();
    }

    public void Add(Item item)
    {
        if (items.Count >= maxCapacity)
            return;

        var newView = Instantiate(itemView, content.transform).GetComponent<ItemView>();
        
        newView.Initialize(this, item.Info);
        items.Add(newView);
    }

    public void Remove(ItemView item)
    {
        var target = items.FirstOrDefault(i => ReferenceEquals(item, i));

        if (target == null)
            return;

        items.Remove(item);
        Destroy(item.gameObject);
    }
}