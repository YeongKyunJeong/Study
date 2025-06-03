using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RSP2
{
    public class InventorySlot : MonoBehaviour
    {
        private InventoryUI inventoryUI;

        [field: SerializeField] public ItemInInventory ItemInSlot { get; set; }

        public void Initialize(InventoryUI _inventoryUI)
        {
            inventoryUI = _inventoryUI;
        }

        public void SetItem(ItemInInventory item)
        {
            ItemInSlot = item;
            item.transform.parent = this.transform;
            item.transform.localPosition = Vector3.zero;
        }

    }
}
