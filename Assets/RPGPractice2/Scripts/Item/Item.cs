using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    public class Item : MonoBehaviour
    {
        [ItemCodeDescription]
        [SerializeField]
        private int _itemCode;
        private SpriteRenderer spriteRenderer;


        public int ItemCode
        {
            get { return _itemCode; }
            set { _itemCode = value; }
        }

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            if (ItemCode != 0)
            {
                Init(ItemCode);
            }
        }

        private void Init(int itemCode)
        {
            if (itemCode != 0)
            {
                ItemCode = itemCode;
                ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);
                spriteRenderer.sprite = itemDetails.itemSprite;

                //Nudge
                if (itemDetails.itemType == ItemType.Reapable_scenary)
                {
                    gameObject.AddComponent<ItemNudge>();
                }
            }
        }
    }
}
