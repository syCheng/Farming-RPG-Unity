using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    public class InventoryManager : SingletonMonobehaviour<InventoryManager>
    {
        private Dictionary<int, ItemDetails> itemDetailsDictionary;


        [SerializeField]
        private SO_ItemList itemList = null;


        protected override void Awake()
        {
            base.Awake();

            CreateItemDetailsDictionary();
        }

        

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
            if (itemDetailsDictionary.TryGetValue(itemCode, out ItemDetails itemDetails1))
            {
                return itemDetails1;
            }
            else
            {
                return null;
            }
        }

    }
}
