using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    public class ItemPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);
                //Debug.Log("<color=#7FFF00><size=12>" + $"{itemDetails.itemDescription}" + "</size></color>");

                //Pick Up
                InventoryManager.Instance.AddItem(item.gameObject, InventoryLocation.player, item);
            }
        }
    }
}
