using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class ItemInInventory : PooledObject, /*IPointerClickHandler,*/ IPointerDownHandler, IPointerUpHandler
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
        }

        public void SetUI(ItemInstance _itemInstance)
        {
            itemInstance = _itemInstance;
            itemImage.sprite = _itemInstance.ItemData.Sprite;
            itemInstance.amountChangeEvent += AmountTextChange;
            AmountTextChange(_itemInstance.amount);

        }

        public void AmountTextChange(int changedAmount)
        {
            if (changedAmount == 1)
            {
                amountTMP.gameObject.SetActive(false);
                return;
            }

            amountTMP.text = changedAmount.ToString();
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    inventoryUI.SelectItem(this);
        //}

        public void OnPointerDown(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inventoryUI.SelectItem(this);
        }
    }
}
