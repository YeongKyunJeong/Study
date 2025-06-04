using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private InventoryUI inventoryUI;
        private ItemInstance itemInstance;
        public ItemInstance ItemInstance { get => itemInstance; }

        [field: SerializeField] private GameObject ItemInSlot { get; set; }
        [field: SerializeField] private Image itemImage;
        [field: SerializeField] private TextMeshProUGUI amountTMP;

        public void Initialize(InventoryUI _inventoryUI)
        {
            inventoryUI = _inventoryUI;
            if (ItemInSlot == null)
            {
                Debug.Log("Item In Slot Not Assigned");
                ItemInSlot = transform.GetChild(1).GetComponent<GameObject>();
            }
            if (itemImage == null)
            {
                Debug.Log("Item Image Not Assigned");
                itemImage = ItemInSlot.transform.GetChild(1).GetComponent<Image>();
            }
            if (amountTMP == null)
            {
                Debug.Log("Amount TMP Not Assigned");
                amountTMP = ItemInSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            }
            ItemInSlot.SetActive(false);
        }

        public void SetItem(ItemInstance newItem)
        {
            ItemInSlot.SetActive(true);
            itemInstance = newItem;
            itemImage.sprite = itemInstance.ItemData.ItemSprite;
            AmountTMPChange(newItem.amount);
            amountTMP.text = newItem.amount.ToString();
            itemInstance.AmountChangeEvent += AmountTMPChange;
        }

        public void ClearSlot(bool isRemoving)
        {
            ItemInSlot.SetActive(false);
            if (isRemoving)
            {
                itemInstance = null;
                amountTMP.text = string.Empty;
                itemInstance.AmountChangeEvent += AmountTMPChange;
            }
        }

        public void AmountTMPChange(int changedAmount)
        {
            if (changedAmount <= 1)
            {
                amountTMP.gameObject.SetActive(false);
                return;
            }

            amountTMP.text = changedAmount.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //inventoryUI.SelectItem(this);
        }
    }
}
