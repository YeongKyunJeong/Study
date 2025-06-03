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
        public event Action<int> amountChangeEvent; 
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
            amountChangeEvent?.Invoke(amount);
            return amount > 0;
        }
    }

    public class Inventory : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;
        InventoryUI inventoryUI;

        List<ItemInstance> items = new List<ItemInstance>();

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            canvasUIManager = gameManager.CanvasUIManager;
            inventoryUI = canvasUIManager.InventoryUI;

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

                    inventoryUI.UpdateItemSlot(item);

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

                items.Add(newItem);
                inventoryUI.AddItemSlot(newItem);

                if (amount <= 0) return true;
            }

            return false;
        }

        public void RemoveItem(ItemInstance itemInstance)
        {
            if (items.Contains(itemInstance))
                items.Remove(itemInstance);
        }

    }
}
