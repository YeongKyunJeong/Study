using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ItemInstance
    {
        public ItemData ItemData;
        public int amount;
        public event Action<int> AmountChangeEvent;
        public bool equipped;

        public ItemInstance(ItemData itemData)
        {
            this.ItemData = itemData;
            amount = 1;
            equipped = false;
        }

        public bool Use(int value = 1)
        {
            amount -= value;
            AmountChangeEvent?.Invoke(amount);
            return amount > 0;
        }
    }

    public class Inventory : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;
        private InventoryUI inventoryUI;

        private List<ItemInstance> items;

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            canvasUIManager = gameManager.CanvasUIManager;
            inventoryUI = canvasUIManager.PanelUI.InventoryUI;

            items = new List<ItemInstance>();

        }

        public bool AddItem(ItemData itemData, int amount = 1)
        {
            if (amount == 0)
                return false;

            for (int i = 0; i < items.Count; i++)
            {
                ItemInstance item = items[i];

                if (item.ItemData == itemData && itemData.CanStack && itemData.MaxStackAmount > item.amount)
                {
                    int diff = Mathf.Min(amount, itemData.MaxStackAmount - item.amount);
                    amount -= diff;
                    item.amount += diff;


                    inventoryUI.PutItemInSlot(item);

                    if (amount <= 0)
                        return true;
                }
            }

            while (amount > 0)
            {
                int diff = Mathf.Min(amount, itemData.MaxStackAmount);
                if (diff <= 0) break;

                ItemInstance newItem = new ItemInstance(itemData);
                newItem.amount = diff;
                amount -= diff;

                if (!inventoryUI.AddItemToSlot(newItem))
                {
                    newItem.amount += amount;
                    Drop(newItem);
                    return false;
                }

                items.Add(newItem);


                if (amount <= 0) return true;
            }

            return false;
        }

        public void Drop(ItemInstance itemInstance)
        {
            ItemData itemData = itemInstance.ItemData;
            Vector3 dropPosition = transform.position + transform.forward * 1.5f + transform.up * 1.5f;

            GameObject go = Instantiate(itemData.DropPrefab, dropPosition, Quaternion.identity);
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 2, ForceMode.Impulse);

            ItemObject itemObject = go.GetComponent<ItemObject>();
            itemObject.amount = itemInstance.amount;
            if (itemObject.itemData == null)
                itemObject.itemData = itemInstance.ItemData;
        }

        public void RemoveItem(ItemInstance itemInstance)
        {
            if (items.Contains(itemInstance))
                items.Remove(itemInstance);
        }

    }
}
