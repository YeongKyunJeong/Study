using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSP2
{

    public class EquipmentSlot : InventorySlot
    {
        [field: SerializeField] private EquipmentType slotType;

        public override void Initialize(InventoryUI _inventoryUI)
        {
            inventoryUI = _inventoryUI;
            if (itemInSlot == null)
            {
                Debug.Log("Item In Slot Not Assigned");
                itemInSlot = transform.GetChild(2).GetComponent<GameObject>();
            }
            if (itemImage == null)
            {
                Debug.Log("Item Image Not Assigned");
                itemImage = itemInSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }

        public override void SetItem(ItemInstance newItem)
        {
            base.SetItem(newItem);
        }

    }
}
