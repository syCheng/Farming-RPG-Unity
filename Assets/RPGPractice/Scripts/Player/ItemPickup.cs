using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class ItemPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = null;
            if (collision.gameObject.TryGetComponent<Item>(out item))
            {
                if (item != null)
                {
                    ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

                    if (itemDetails != null)
                    {
                        //Debug.Log("<color=#7FFF00><size=12>" + $"{itemDetails.itemDescription}" + "</size></color>");
                        if (itemDetails.canBePickedUp)
                        {
                            InventoryManager.Instance.AddItem(InventoryLocation.player, item, item.gameObject);
                        }
                    }

                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
        }
    }
}
