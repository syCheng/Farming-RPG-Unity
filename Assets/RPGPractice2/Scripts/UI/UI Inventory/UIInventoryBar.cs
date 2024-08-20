using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    //物品栏
    //1.实时检查Player位置 根据位置判断是否在底部
    public class UIInventoryBar : MonoBehaviour
    {

        [SerializeField] private Sprite blank16x16Sprite = null;


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
