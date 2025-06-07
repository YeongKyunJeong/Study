using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IPointerUpHandler
    {
        private InventoryUI inventoryUI;
        private ItemInstance itemInstance;
        public ItemInstance ItemInstance { get => itemInstance; }

        [field: SerializeField] private GameObject ItemInSlot { get; set; }
        [field: SerializeField] private Image itemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI amountTMP { get; set; }
        [field: SerializeField] private GameObject selectedFrame { get; set; }

        public event Action<InventorySlot, bool> ClickEvent;
        public event Action<InventorySlot> DragBeginEvent;
        public event Action<InventorySlot> PointerUpEvent;

        public void Initialize(InventoryUI _inventoryUI)
        {
            inventoryUI = _inventoryUI;
            if (ItemInSlot == null)
            {
                Debug.Log("Item In Slot Not Assigned");
                ItemInSlot = transform.GetChild(2).GetComponent<GameObject>();
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
            if (selectedFrame == null)
            {
                Debug.Log("Selected Not Assigned");
                selectedFrame = transform.GetChild(1).GetComponent<GameObject>();
            }
            ItemInSlot.SetActive(false);
            selectedFrame.SetActive(false);
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
                itemInstance.AmountChangeEvent -= AmountTMPChange;
                selectedFrame.SetActive(false);
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

        public void CloseInventory()
        {
            selectedFrame.SetActive(false);
            // TO DO :: Add logic called when inventory closed
        }

        public void OnPointerClick(PointerEventData eventData)
        {

            bool isSelectedBefore = selectedFrame.activeSelf;
            ClickEvent?.Invoke(this, isSelectedBefore);
            selectedFrame.SetActive(!isSelectedBefore);
            // TO DO :: SelectItem
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemInstance == null) return;

            ClearSlot(false);
            DragBeginEvent?.Invoke(this);
        }

        public void SetActiveOfSelectedFram(bool isOn)
        {
            selectedFrame.SetActive(isOn);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent?.Invoke(this);
            // TO DO :: Drop Item
        }
    }
}
