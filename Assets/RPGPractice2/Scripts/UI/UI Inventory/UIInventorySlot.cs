using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmingRPG2
{


    /// <summary>
    /// 物品槽位
    /// </summary>
    public class UIInventorySlot : MonoBehaviour
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






        
    }
}
