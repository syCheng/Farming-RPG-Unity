using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarmingRPG2
{

    [CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
    public class ItemCodeDescriptionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //return base.GetPropertyHeight(property, label);
            return EditorGUI.GetPropertyHeight(property)*3;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 3), label, property.intValue);

                EditorGUI.LabelField(new Rect(position.x,position.y+position.height/3,position.width,position.height/3), "Item Description", GetItemDescription(property.intValue));


                Sprite sprite = GetItemSprite(property.intValue);
                if (sprite != null)
                {
                    EditorGUI.DrawTextureTransparent(new Rect(position.x+10, position.y + (position.height / 3) * 2, sprite.texture.width, sprite.texture.height), sprite.texture);
                }
                

                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = newValue;
                }
            }

            EditorGUI.EndProperty();
        }


        private Sprite GetItemSprite(int itemCode)
        {
            SO_ItemList sO_ItemList;
            sO_ItemList = AssetDatabase.LoadAssetAtPath<SO_ItemList>("Assets/RPGPractice2/SO/RPG2SO_itemList.asset") as SO_ItemList;
            ItemDetails itemDetails = sO_ItemList.itemDetails.Find(x => x.itemCode == itemCode);
            if (itemDetails != null)
            {
                return itemDetails.itemSprite;
            }
            else
            {
                return null;
            }
        }


        private string GetItemDescription(int intValue)
        {
            SO_ItemList sO_ItemList;
            sO_ItemList = AssetDatabase.LoadAssetAtPath<SO_ItemList>("Assets/RPGPractice2/SO/RPG2SO_itemList.asset") as SO_ItemList;
            ItemDetails itemDetails = sO_ItemList.itemDetails.Find(x=>x.itemCode == intValue);
            if (itemDetails != null)
            {
                return itemDetails.itemDescription;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
