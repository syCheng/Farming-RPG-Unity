using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

namespace FarmingRPG2
{
    public class InventoryManager : SingletonMonobehaviour<InventoryManager>
    {
        private Dictionary<int, ItemDetails> itemDetailsDictionary;



        [SerializeField]
        private SO_ItemList itemList = null;


        //hold all inventory Items 
        public List<InventoryItem>[] inventoryLists;


        [HideInInspector]
        public int[] inventoryListCapacityIntArray; 



        protected override void Awake()
        {
            base.Awake();
            CreateItemDetailsDictionary();
            CreateInventoryList();
        }

      


        /// <summary>
        /// Populates the itemDetailsDictionary from the scriptable object Items List.
        /// </summary>
        private void CreateItemDetailsDictionary()
        {
            itemDetailsDictionary = new Dictionary<int, ItemDetails>();

            foreach (ItemDetails itemDetail in itemList.itemDetails)
            {
                itemDetailsDictionary.Add(itemDetail.itemCode, itemDetail);
            }
        }



        /// <summary>
        /// return the ItemDetails (from SO_ItemList) for the itemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
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



















        //创建InventoryList
        private void CreateInventoryList()
        {
            inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

            //index 0 : Player    //index 1 : Chests
            for (int i = 0; i < inventoryLists.Length; i++)
            {
                inventoryLists[i] = new List<InventoryItem>();
            }

            inventoryListCapacityIntArray = new int[inventoryLists.Length];

            //初始化角色物品栏个数
            inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
        }



        public void AddItem(GameObject itemObj,InventoryLocation inventoryLocation,Item item)
        {
            AddItem(inventoryLocation, item);
            Destroy(itemObj);
        }



        //添加物品
        public void AddItem(InventoryLocation inventoryLocation,Item item)
        {
            int itemCode = item.ItemCode;
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

            //先去物品栏查找物品  
            int itemPosition = FindItemInInventory(inventoryLocation,itemCode);


            if (itemPosition == -1)
            {
                //添加新物品
                AddItemAtPosition(inventoryList, itemCode);
            }
            else
            {
                //已有改物品 数量+1
                AddItemAtPosition(inventoryList,itemCode,itemPosition);    
            }


            DebugPrintItemList(inventoryLocation);

            //物品改变事件触发
            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }


        private void DebugPrintItemList(InventoryLocation inventoryLocation)
        {
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];
            string debugStr = string.Empty;
            for (int i = 0; i < inventoryList.Count; i++)
            {
                debugStr += $"{GetItemDetails(inventoryList[i].itemCode).itemDescription} : [{inventoryList[i].itemQuantity}]" + "\n";
                ;
            }
            Debug.Log("<color=#7FFF00><size=12>" + $"{debugStr}" + "</size></color>");
        }

        private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int itemPosition)
        {
            InventoryItem inventoryItem = new InventoryItem();
            inventoryItem.itemCode = itemCode;
            inventoryItem.itemQuantity = (inventoryList[itemPosition].itemQuantity+1);
            inventoryList[itemPosition] = inventoryItem;
        }


        private void AddItemAtPosition(List<InventoryItem> inventoryList,int itemCode)
        {
            InventoryItem inventoryItem = new InventoryItem();
            inventoryItem.itemCode = itemCode;
            inventoryItem.itemQuantity = 1;
            inventoryList.Add(inventoryItem);
        }







        private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
        {
            List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i].itemCode == itemCode) return i;
            }

            return -1;
        }
    }
}
