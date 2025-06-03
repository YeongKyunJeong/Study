using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RSP2
{
    public class InventoryUI : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager uIManager;
        private Player player;


        [field: SerializeField] private GameObject itemSlotPrefab;
        [field: SerializeField] private Transform contentRoot;

        private List<ItemInInventory> items = new List<ItemInInventory>();
        [field: SerializeField] private InventorySlot[] inventorySlots;
        [field: SerializeField] private ItemInInventory selectedItem;

        [field: SerializeField] private Button equipButton;
        [field: SerializeField] private Button useButton;
        [field: SerializeField] private Button dropButton;


        public void InitializeUI(GameManager _gameManager, CanvasUIManager _uIManager)
        {
            gameManager = _gameManager;
            uIManager = _uIManager;
            player = _gameManager.Player;

            gameObject.SetActive(false);

            equipButton.onClick.AddListener(OnEquipButton);
            useButton.onClick.AddListener(OnUseButton);
            dropButton.onClick.AddListener(OnDropButton);
        }

        public void Open()
        {
            selectedItem = null;
            UpdateButtons(null);
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void AddItemSlot(ItemInstance item)
        {
            GameObject go = Instantiate(itemSlotPrefab, contentRoot);
            // TO DO :: Add object pooling logic to add new item 
            ItemInInventory itemInSlot = go.GetComponent<ItemInInventory>();
            itemInSlot.Initialize(this);
            itemInSlot.SetUI(item);
            items.Add(itemInSlot);


            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].ItemInSlot == null)
                {
                    inventorySlots[i].SetItem(itemInSlot);
                    break;
                }
            }

            return;

        }

        public void UpdateItemSlot(ItemInstance item)
        {
            ItemInInventory slot = items.First(slot => slot.ItemInstance == item);

            if (slot == null) return;

            slot.SetUI(item);
        }

        public void SelectItem(ItemInInventory slot)
        {
            selectedItem = slot;
            UpdateButtons(slot.ItemInstance);
        }

        public void UpdateButtons(ItemInstance item)
        {
            if (item == null)
            {
                useButton.interactable = false;
                equipButton.interactable = false;
                dropButton.interactable = false;
                return;
            }

            switch (item.ItemData.Type)
            {
                case ItemType.Consumable:
                    useButton.interactable = true;
                    equipButton.interactable = false;
                    break;
                case ItemType.Equipable:
                    useButton.interactable = false;
                    equipButton.interactable = true;
                    break;
            }
            dropButton.interactable = true;
        }

        public void OnUseButton()
        {
            if (selectedItem == null) return;

            ConsumableData ConsumableData = selectedItem.ItemInstance.ItemData as ConsumableData;

            if (ConsumableData == null) return;

            for (int i = 0; i < ConsumableData.ConsumableEffects.Length; i++)
            {
                switch (ConsumableData.ConsumableEffects[i].ConsumableType)
                {
                    case ConsumableType.HPHealing: // TODO :: Make separate healing logic
                        {
                            player.CombatSystem.ChangeHealth(ConsumableData.ConsumableEffects[i].effectValue);
                            break;
                        }
                }
            }

            SFXManager.PlayClip(ConsumableData.UsageSoundClip, player.transform.position);

            if (selectedItem.ItemInstance.Use() == false)
            {
                items.Remove(selectedItem);
                Destroy(selectedItem.gameObject);
                selectedItem = null;
                UpdateButtons(null);
            }
            else
            {
                selectedItem.SetUI(selectedItem.ItemInstance);
            }
        }

        public void OnEquipButton()
        {
            if (selectedItem == null) return;

            player.EquipItem(selectedItem.ItemInstance);
            SFXManager.PlayClip(selectedItem.ItemInstance.ItemData.UsageSoundClip, player.transform.position);
        }
        public void OnDropButton()
        {
            if (selectedItem == null) return;

            if (selectedItem.ItemInstance.equipped)
                return;

            Drop(selectedItem.ItemInstance);

            items.Remove(selectedItem);
            player.Inventory.RemoveItem(selectedItem.ItemInstance);
            Destroy(selectedItem.gameObject);

            UpdateButtons(null);
        }

        void Drop(ItemInstance itemInstance)
        {
            ItemData itemData = itemInstance.ItemData;
            Vector3 dropPosition = player.transform.position + player.transform.forward * 1.5f + player.transform.up * 1.5f;

            GameObject go = Instantiate(itemData.DropPrefab, dropPosition, Quaternion.identity);
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            rigidbody.AddForce(player.transform.forward * 2, ForceMode.Impulse);

            ItemObject itemObject = go.GetComponent<ItemObject>();
            itemObject.amount = itemInstance.amount;
            if (itemObject.itemData == null)
                itemObject.itemData = itemInstance.ItemData;
        }
    }
}
