using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace FarmingRPG
{
    public class UIInventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {

        private Camera mainCamera; //screen to world point
        private Canvas parentCanvas;

        [SerializeField] private GameObject inventoryTextBoxPrefab=null;

        private Transform parentItem; //set to the item gameobj
        private GameObject draggedItem; 


        public Image inventorySlotHighlight;
        public Image inventorySlotImage;
        public TextMeshProUGUI textMeshProUGUI;

        [SerializeField] private UIInventoryBar inventoryBar = null;
        [SerializeField] private GameObject itemPrefab = null;

        [SerializeField] public bool isSelected = false;

        [HideInInspector] public ItemDetails itemDetails;
        [HideInInspector] public int itemQuantity;


        // Used to swap items inside inventory bar
        [SerializeField] private int slotNumber = 0;





        private void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();
        }


        private void Start()
        {
            mainCamera = Camera.main;
            parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
            inventoryBar = GetComponentInParent<UIInventoryBar>();
        }


        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (itemDetails != null)
            {
                Player.Instance.DisablePlayerInputAndMovement();

                draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

                //Get Image for dragged item
                Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
                draggedItemImage.sprite = inventorySlotImage.sprite;

                SetSelectedItem();
            }
        }



        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (draggedItem != null)
            {
                draggedItem.transform.position = Input.mousePosition;
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (draggedItem != null)
            {
                Destroy(draggedItem);

                if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
                {
                    //swap
                    int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().slotNumber;

                    InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);


                    if (inventoryBar.inventoryTextBoxGameObject != null)
                    {
                        Destroy(inventoryBar.inventoryTextBoxGameObject);
                    }

                    ClearSelectedItem();
                }
                else
                {
                    if (itemDetails.canBeDropped)
                    {
                        DropSelectedItemAtMousePosition();
                    }
                }

                Player.Instance.EnablePlayerInputs();
            }
        }



        private void DropSelectedItemAtMousePosition()
        {
            if (itemDetails != null && isSelected)
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-mainCamera.transform.position.z));

                GameObject itemGameObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);

                Item item = itemGameObject.GetComponent<Item>();
                item.ItemCode = itemDetails.itemCode;

                //Remove players inventory
                InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);

                if (InventoryManager.Instance.FindItemInInventory(InventoryLocation.player, item.ItemCode) == -1)
                {
                    ClearSelectedItem();
                }
            }
        }

        //show inventory text box
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (itemQuantity != 0)
            {
                inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);
                inventoryBar.inventoryTextBoxGameObject.transform.SetParent(parentCanvas.transform, false);

                UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

                string itemTypeDescription = InventoryManager.Instance.GetItemTypeDescription(itemDetails.itemType);

                inventoryTextBox.SetTextboxText(itemDetails.itemDescription, itemTypeDescription, "", itemDetails.itemLongDescription, "", "");

                if (inventoryBar.IsInventoryBarPositionBottom)
                {
                    inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                    inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 14f, transform.position.z);
                }
                else
                {
                    inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                    inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 14f, transform.position.z);
                }

            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (inventoryBar.inventoryTextBoxGameObject != null)
            {
                Destroy(inventoryBar.inventoryTextBoxGameObject);
            }
        }



        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (isSelected == true)
                {
                    ClearSelectedItem();
                }
                else
                {
                    if (itemQuantity > 0)
                    {
                        SetSelectedItem();
                    }
                }
            }
        }



        private void SetSelectedItem()
        {

            inventoryBar.ClearHighlightOnInventorySlot();

            isSelected = true;

            inventoryBar.SetHighlightedInventorySlots();

            InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, itemDetails.itemCode);

        }



        private void ClearSelectedItem()
        {
            inventoryBar.ClearHighlightOnInventorySlot();
            isSelected = false;

            InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);
        }
    }
}
