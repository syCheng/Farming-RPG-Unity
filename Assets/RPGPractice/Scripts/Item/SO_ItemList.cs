using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    [CreateAssetMenu(fileName = "so_itemList",menuName = "SO/Item/ItemList")]
    public class SO_ItemList:ScriptableObject
    {
        [SerializeField]
        public List<ItemDetails> itemDetails;
    }
}
