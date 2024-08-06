using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class Item : MonoBehaviour
    {
        [ItemCodeDescription]
        [SerializeField]
        private int _itemCode;
        public int ItemCode { get => _itemCode; set { _itemCode = value; } }



        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        }


        private void Start()
        {
            if (_itemCode != 0)
            {
                Init(ItemCode);
            }
        }

        public void Init(int itemCodeParam)
        {
            if (itemCodeParam != 0)
            {
                ItemCode = itemCodeParam;
                ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCodeParam);
                spriteRenderer.sprite = itemDetails.itemSprite;

                if (itemDetails.itemType == ItemType.Reapable_scenary)
                {
                    gameObject.AddComponent<ItemNudge>();
                }
            }
        }
    }
}
