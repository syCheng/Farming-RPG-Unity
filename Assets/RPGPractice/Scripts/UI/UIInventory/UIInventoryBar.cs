using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class UIInventoryBar : MonoBehaviour
    {



        [SerializeField] private Sprite blank16x16Sprite = null;
        [SerializeField] private UIInventorySlot[] inventorySlot = null;


        public GameObject inventoryBarDraggedItem;


        [HideInInspector] public GameObject inventoryTextBoxGameObject;

        private RectTransform rectTransform;

        private bool _isInventoryBarPositionBottom = true;

        public bool IsInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }



        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

        }


        private void OnEnable()
        {
            EventHandler.InventoryUpdateEvent += InventoryUpdated;
        }

        private void OnDisable()
        {
            EventHandler.InventoryUpdateEvent -= InventoryUpdated;
        }



        private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
        {
            if (inventoryLocation == InventoryLocation.player)
            {
                ClearInventorySlots();

                if (inventorySlot.Length > 0 && inventoryList.Count > 0)
                {
                    for (int i = 0; i < inventorySlot.Length; i++)
                    {
                        if (i < inventoryList.Count)
                        {
                            int itemCode = inventoryList[i].itemCode;
                            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);
                            if (itemDetails != null)
                            {
                                inventorySlot[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                                inventorySlot[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                                inventorySlot[i].itemDetails = itemDetails;
                                inventorySlot[i].itemQuantity = inventoryList[i].itemQuantity;

                                SetHighlightedInventorySlots();
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
            if (inventorySlot.Length > 0)
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    inventorySlot[i].inventorySlotImage.sprite = blank16x16Sprite;
                    inventorySlot[i].textMeshProUGUI.text = string.Empty;
                    inventorySlot[i].itemDetails = null;
                    inventorySlot[i].itemQuantity = 0;

                    SetHighlightedInventorySlots();
                }
            }
        }


        private void Update()
        {
            SwitchInventoryBarPosition();
        }

        public void ClearHighlightOnInventorySlot()
        {
            if (inventorySlot.Length > 0)
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i].isSelected)
                    {
                        inventorySlot[i].isSelected = false;
                        inventorySlot[i].inventorySlotHighlight.color = new Color(0f,0f,0f,0f);

                        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);
                    }
                }
            }
        }


        public void SetHighlightedInventorySlots()
        {
            if (inventorySlot.Length > 0)
            {
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (inventorySlot[i].isSelected)
                    {
                        SetHighlightedInventorySlots(i);
                    }
                }
            }
        }


        public void SetHighlightedInventorySlots(int itemPosition)
        {
            if (inventorySlot.Length > 0 && inventorySlot[itemPosition].itemDetails != null)
            {
                if (inventorySlot[itemPosition].isSelected)
                {
                    inventorySlot[itemPosition].inventorySlotHighlight.color = new Color(1f, 1f,1f, 1f);

                    InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, inventorySlot[itemPosition].itemDetails.itemCode);
                }
            }
        }




        private void SwitchInventoryBarPosition()
        {
            Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();

            //Debug.Log("<color=#7FFF00><size=12>" + $"{playerViewportPosition}" + "</size></color>");

            if (playerViewportPosition.y > 0.3f && IsInventoryBarPositionBottom == false)
            {
                rectTransform.pivot = new Vector2(0.5f, 0f);
                rectTransform.anchorMin = new Vector2(0.5f, 0);
                rectTransform.anchorMax = new Vector2(0.5f, 0);
                rectTransform.anchoredPosition = new Vector2(0f, 2.5f);

                IsInventoryBarPositionBottom = true;
            }
            else if (playerViewportPosition.y <= 0.3f && IsInventoryBarPositionBottom == true)
            {
                rectTransform.pivot = new Vector2(0.5f, 1f);
                rectTransform.anchorMin = new Vector2(0.5f, 1);
                rectTransform.anchorMax = new Vector2(0.5f, 1);
                rectTransform.anchoredPosition = new Vector2(0f, -2.5f);

                IsInventoryBarPositionBottom = false;
            }
        }



       

    }
}
