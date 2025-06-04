using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class MovingSlot : MonoBehaviour
    {
        private InventoryUI inventoryUI;
        private ItemInstance itemInstance;
        public ItemInstance ItemInstance { get { return itemInstance; } }

        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountTMP;


        public void Initialize(InventoryUI _inventoryUI)
        {
            inventoryUI = _inventoryUI;
            if (itemImage == null)
            {
                Debug.Log("Item Image Not Assigned");
                itemImage = transform.GetChild(1).GetComponent<Image>();
            }
            if (amountTMP == null)
            {
                Debug.Log("Amount TMP Not Assigned");
                amountTMP = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            }
            gameObject.SetActive(false);
        }

        public void CarryItem(ItemInstance newItem)
        {
            gameObject.SetActive(true);
            itemInstance = newItem;
            itemImage.sprite = itemInstance.ItemData.ItemSprite;

            if (newItem.amount <= 1)
            {
                amountTMP.gameObject.SetActive(false);
                return;
            }

            amountTMP.gameObject.SetActive(true);
            amountTMP.text = newItem.amount.ToString();
        }

        public ItemInstance DropItem()
        {
            gameObject.SetActive(false);
            itemInstance = null;
            return itemInstance;
        }
    }
}
