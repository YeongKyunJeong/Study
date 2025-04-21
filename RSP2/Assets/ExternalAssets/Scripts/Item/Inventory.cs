using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ItemInstance
    {
        public ItemData ItemData;
        public int amount;
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
            return amount > 0;
        }
    }

    public class Inventory : MonoBehaviour
    {
        GameManager _gameManager;
        //UIManager _uiManager;
        //InventoryUI _inventoryUI;

        List<ItemInstance> items = new List<ItemInstance>();

        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
            //_uiManager = gameManager.UIManager;
            //_inventoryUI = _uiManager.InventoryUI;

        }

        public bool AddItem(ItemData itemData, int amount = 1)
        {
            if (amount == 0)
                return false;

            for (int i = 0; i < items.Count; i++)
            {
                ItemInstance item = items[i];

                if (item.ItemData == itemData && itemData.canStack && itemData.maxStackAmount > item.amount)
                {
                    int diff = Mathf.Min(amount, itemData.maxStackAmount - item.amount);
                    amount -= diff;
                    item.amount += diff;

                    //_inventoryUI.UpdateItemSlot(item);

                    if (amount <= 0)
                        return true;
                }
            }

            while (amount > 0)
            {
                int diff = Mathf.Min(amount, itemData.maxStackAmount);
                if (diff <= 0) break;

                ItemInstance newItem = new ItemInstance(itemData);
                newItem.amount = diff;
                amount -= diff;

                items.Add(newItem);
                //_inventoryUI.AddItemSlot(newItem);

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
