using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    [CreateAssetMenu(fileName = "RPG2SO_itemList",menuName = "RPG2SO/Items/ItemList")]
    public class SO_ItemList : ScriptableObject
    {
        [SerializeField]
        public List<ItemDetails> itemDetails;
    }
}
