using FarmingRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    //物品栏
    //1.实时检查Player位置 根据位置判断是否在底部
    //2.监听EventHandler.InventoryUpdatedEvent 刷新显示
    //3.拖拽Slot后Drop物品 刷新
    public class UIInventoryBar : MonoBehaviour
    {

        [SerializeField] private Sprite blank16x16Sprite = null;


        [SerializeField] private UIInventorySlot[] uiInventorySlots;


        public GameObject inventoryBarDraggedItem;


        private RectTransform rectTransform;


        private bool isInventoryBarPositionBottom = true;
        public bool IsInventoryBarPositionBottom
        {
            get { return IsInventoryBarPositionBottom; }
            set { IsInventoryBarPositionBottom = value; }
        }


        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }


        private void OnEnable()
        {
            EventHandler.InventoryUpdatedEvent += EventHandler_InventoryUpdatedEvent;
        }

       

        private void OnDisable()
        {
            EventHandler.InventoryUpdatedEvent -= EventHandler_InventoryUpdatedEvent;
        }



        private void EventHandler_InventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
        {
            if (inventoryLocation == InventoryLocation.player)
            {
                //清除所有Slots
                ClearInventorySlots();

                if (uiInventorySlots.Length > 0 && inventoryList.Count > 0)
                {
                    for (int i = 0; i < uiInventorySlots.Length; i++)
                    {
                        if (i < inventoryList.Count)
                        {
                            int itemCode = inventoryList[i].itemCode;

                            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);

                            if (itemDetails != null)
                            {
                                uiInventorySlots[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                                uiInventorySlots[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                                uiInventorySlots[i].itemDetails = itemDetails;
                                uiInventorySlots[i].itemQuantity = inventoryList[i].itemQuantity;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void ClearInventorySlots()
        {
            if (uiInventorySlots.Length > 0)
            {
                // loop through inventory slots and update with blank sprite
                for (int i = 0; i < uiInventorySlots.Length; i++)

                {
                    uiInventorySlots[i].inventorySlotImage.sprite = blank16x16Sprite;
                    uiInventorySlots[i].textMeshProUGUI.text = "";
                    uiInventorySlots[i].itemDetails = null;
                    uiInventorySlots[i].itemQuantity = 0;
                }
            }
        }



        // Update is called once per frame
        void Update()
        {
            SwitchInventoryBarPosition();
        }

        private void SwitchInventoryBarPosition()
        {
            Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();

            if (playerViewportPosition.y > 0.3f && isInventoryBarPositionBottom == false)
            {
                rectTransform.pivot = new Vector2(0.5f, 0);
                rectTransform.anchorMin = new Vector2(0.5f, 0f);
                rectTransform.anchorMax = new Vector2(0.5f, 0f);
                rectTransform.anchoredPosition = new Vector2(0, 2.5f);
                isInventoryBarPositionBottom = true;
            }
            else if (playerViewportPosition.y <= 0.3f && isInventoryBarPositionBottom)
            {
                rectTransform.pivot = new Vector2(0.5f, 1f);
                rectTransform.anchorMin = new Vector2(0.5f, 1f);
                rectTransform.anchorMax = new Vector2(0.5f, 1f);
                rectTransform.anchoredPosition = new Vector2(0, -2.5f);
                isInventoryBarPositionBottom = false;
            }
        }


        //pick up the item  by script


    }
}
