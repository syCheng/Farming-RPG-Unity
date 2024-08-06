using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class InventoryManager : SingletonMonobehaviour<InventoryManager>
    {
        private Dictionary<int, ItemDetails> itemDetailsDictionary;

        //keep track of inventory selection
        private int[] selectedInventoryItem;    //the index of the array is the inventory list, 0->player 1->chest ,  the value is itemCode.


        [SerializeField]
        private SO_ItemList itemList = null;


        //0-player 1-chest
        public List<InventoryItem>[] inventoryLists;


        [HideInInspector]  //[0] -> player capacity   [1] -> chest capacity
        public int[] inventoryListCapacityIntArray;


        



        protected override void Awake()
        {
            base.Awake();


            //Create Inventory lists
            CreateInventoryLists();


            //create item details dictionary
            CreateItemDetailsDictionary();


            //Initialize current selected item array
            selectedInventoryItem = new int[(int)InventoryLocation.count];

            for (int i = 0; i < selectedInventoryItem.Length; i++)
            {
                selectedInventoryItem[i] = -1; //selection initial value is -1 ,reapresent that there is no item selected.
            }
        }


        private void CreateInventoryLists()
        {
            inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
            for (int i = 0; i < (int)InventoryLocation.count; i++)
            {
                inventoryLists[i] = new List<InventoryItem>();
            }

            inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

            inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
        }






        public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectDelete)
        {
            AddItem(inventoryLocation,item);
            Destroy(gameObjectDelete);
        }


        public void AddItem(InventoryLocation inventoryLocation,Item item)
        {
            int itemCode = item.ItemCode;
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];


            //check if the inventory already contain the item
            int itemPosition = FindItemInInventory(inventoryLocation, itemCode);


            if (itemPosition != -1)
            {
                AddItemAtPosition(inventoryList, itemCode, itemPosition);
            }
            else
            {
                AddItemAtPosition(inventoryList,itemCode);
            }

            //send event that inventory has been updated.
            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }

       

        public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
        {
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i].itemCode == itemCode)
                {
                    return i;
                }
            }
            return -1;
        }


        private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
        {
            InventoryItem inventoryItem = new InventoryItem();

            inventoryItem.itemCode = itemCode;
            inventoryItem.itemQuantity = 1;
            inventoryList.Add(inventoryItem);

            DebugPrintInventoryList(inventoryList);
        }

        private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int itemPosition)
        {
            InventoryItem inventoryItem = inventoryList[itemPosition];
            inventoryItem.itemQuantity += 1;

            inventoryList[itemPosition] = inventoryItem;

            DebugPrintInventoryList(inventoryList);
        }



        private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
        {
            string debugStr = string.Empty;
            for (int i = 0; i < inventoryList.Count; i++)
            {
                int code = inventoryList[i].itemCode;
                ItemDetails itemDetails = GetItemDetails(code);
                debugStr += string.Format("{0}:[{1}]", itemDetails.itemDescription, inventoryList[i].itemQuantity)+",";
            }
            Debug.Log("<color=#7FFF00><size=12>" + $"{debugStr}" + "</size></color>");
        }




        private void CreateItemDetailsDictionary()
        {
            itemDetailsDictionary = new Dictionary<int, ItemDetails>();
            itemList.itemDetails.ForEach(item =>
            {
                itemDetailsDictionary.Add(item.itemCode, item);
            });
        }


        public ItemDetails GetItemDetails(int itemCode)
        {
            if (itemDetailsDictionary.TryGetValue(itemCode, out ItemDetails itemDetails))
            {
                return itemDetails;
            }
            else
            {
                return null;
            }
        }



        public string GetItemTypeDescription(ItemType itemType)
        {
            string itemTypeDescription;
            switch (itemType)
            {

                case ItemType.Watering_tool:
                    itemTypeDescription = Settings.WateringTool;
                    break;

                case ItemType.Hoeing_tool:
                    itemTypeDescription = Settings.HoeingTool;
                    break;

                case ItemType.Chopping_tool:
                    itemTypeDescription = Settings.ChoppingTool;
                    break;

                case ItemType.Breaking_tool:
                    itemTypeDescription = Settings.BreakingTool;
                    break;

                case ItemType.Reaping_tool:
                    itemTypeDescription = Settings.ReapingTool;
                    break;

                case ItemType.Collecting_tool:
                    itemTypeDescription = Settings.CollectingTool;
                    break;

               
                default:
                    itemTypeDescription = itemType.ToString();
                    break;
            }

            return itemTypeDescription;
        }




        public void RemoveItem(InventoryLocation inventoryLocation,int itemCode)
        {
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

            int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

            if (itemPosition != -1)
            {
                RemoveItemAtPosition(inventoryList,itemCode,itemPosition);
            }


            //the inventory has been updated.
            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);

        }


        private void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int itemPosition)
        {
            InventoryItem inventoryItem = new InventoryItem();
            int quantity = inventoryList[itemPosition].itemQuantity - 1;
            if (quantity > 0)
            {
                inventoryItem.itemQuantity = quantity;
                inventoryItem.itemCode = itemCode;
                inventoryList[itemPosition] = inventoryItem;
            }
            else
            {
                inventoryList.RemoveAt(itemPosition);
            }
        }


        internal void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
        {
            if (fromItem < inventoryLists[(int)inventoryLocation].Count &&
                toItem < inventoryLists[(int)inventoryLocation].Count &&
                fromItem != toItem && fromItem >= 0 && toItem >= 0)
            {
                InventoryItem fromInventoryItem = inventoryLists[(int)inventoryLocation][fromItem];
                InventoryItem toInventoryItem = inventoryLists[(int)inventoryLocation][toItem];

                inventoryLists[(int)inventoryLocation][toItem] = fromInventoryItem;
                inventoryLists[(int)inventoryLocation][fromItem] = toInventoryItem;

                EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
            }
        }


        //set the selected item in inventory
        public void SetSelectedInventoryItem(InventoryLocation inventoryLocation,int itemCode)
        {
            selectedInventoryItem[(int)inventoryLocation] = itemCode;
        }


        //not selected.
        public void ClearSelectedInventoryItem(InventoryLocation inventoryLocation)
        {
            selectedInventoryItem[(int)inventoryLocation] = -1;
        }

    }
}
