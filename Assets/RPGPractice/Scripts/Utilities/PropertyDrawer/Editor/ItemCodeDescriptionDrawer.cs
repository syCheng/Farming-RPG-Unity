using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarmingRPG
{
    [CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
    public class ItemCodeDescriptionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //change property height

            //return base.GetPropertyHeight(property, label);

            return EditorGUI.GetPropertyHeight(property) * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.BeginChangeCheck();//check the change values

                //draw item part
                var newValue = EditorGUI.IntField(new Rect(position.x,position.y,position.width,position.height/2),label,property.intValue);

                //draw item description
                EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item  Description",GetItemDescription(property.intValue));


                //if item code value changed set new value
                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = newValue;
                }
            }

            EditorGUI.EndProperty();
        }



        private string GetItemDescription(int itemCode)
        {
            SO_ItemList so_itemList;
            so_itemList = AssetDatabase.LoadAssetAtPath<SO_ItemList>("Assets/RPGPractice/SO/Item/so_itemList.asset") as SO_ItemList;
            ItemDetails itemDetails = so_itemList.itemDetails.Find(x => x.itemCode == itemCode);
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
