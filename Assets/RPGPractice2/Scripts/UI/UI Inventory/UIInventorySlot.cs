using Cinemachine.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmingRPG2
{


    /// <summary>
    /// 物品槽位
    /// </summary>
    public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        private Camera mainCamera;
        private Canvas parentCanvas;
        private Transform parentItem;
        private GameObject draggedItem;

        public Image inventorySlotHighlight;
        public Image inventorySlotImage;
        public TextMeshProUGUI textMeshProUGUI;


        [SerializeField]
        private UIInventoryBar inventoryBar;
        public ItemDetails itemDetails;
        [SerializeField] private GameObject itemPrefab = null;
        public int itemQuantity;


        [SerializeField]
        private int slotNumber;


        [SerializeField]
        private GameObject inventoryTextBoxPrefab = null;


        private void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();
        }


        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
            parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
        }


        //开始拖拽  
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemDetails != null)
            {
                Player.Instance.DisablePlayerInputAndResetMovement();

                //drag to instantiate the dragged item
                draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

                Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
                draggedItemImage.sprite = inventorySlotImage.sprite;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggedItem != null)
            {
                draggedItem.transform.position = Input.mousePosition;
            }
        }



        public void OnEndDrag(PointerEventData eventData)
        {
            if (draggedItem != null)
            {
                Destroy(draggedItem);

                if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
                {

                    //Swap the item.
                    //Current Camera Position. 
                    UIInventorySlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>();
                    int toSlotNumber = targetSlot.slotNumber;

                    //Swap item.
                    //InventoryManager.Instance.SwapInventoryItem();

                    //Get the item component .

                }
                else
                {
                    if (itemDetails.canBeDropped)
                    {
                        //Drop selected item  DropSelectedItemAtMousePosition
                        DropSelectedItemAtMousePosition();
                    }
                }


                Player.Instance.EnablePlayerInput();
            }
        }






        private void DropSelectedItemAtMousePosition()
        {
            if (itemDetails != null)
            {

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-mainCamera.transform.position.z));

                GameObject itemGameObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity);
                Item item = itemGameObject.GetComponent<Item>();
                item.ItemCode = itemDetails.itemCode;

                // Remove item from players inventory
                InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);
            }
        }
    }
}
