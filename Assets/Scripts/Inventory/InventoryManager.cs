using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    [SerializeField] private SO_ItemList itemList = null;

    // Awake method gets called before the 'start' method inside 'item'
    protected override void Awake()
    {
        base.Awake();
        CreateItemDetailsDictionary();
    }

    // Populate the dictionary from the scriptable object item list
    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetail in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetail.itemCode, itemDetail);
        }
    }

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails)) {
            return itemDetails;
        } else
        {
            return null;
        }
    }
}
