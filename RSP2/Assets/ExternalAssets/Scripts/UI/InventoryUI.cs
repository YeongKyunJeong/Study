using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RSP2
{
    public class InventoryUI : MonoBehaviour
    {
        private CanvasUIManager uIManager;
        private Player player;

        List<InventorySlot> itemSlots = new List<InventorySlot>();

        [SerializeField] private GameObject itemSlotPrefab;
        [SerializeField] private Transform contentRoot;

        [SerializeField] private Button equipButton;
        [SerializeField] private Button useButton;
        [SerializeField] private Button dropButton;

        private InventorySlot selectedItem;


        public void InitializeUI(GameManager gameManager, CanvasUIManager uIManager)
        {
            this.uIManager = uIManager;
            player = gameManager.Player;

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
            InventorySlot slot = go.GetComponent<InventorySlot>();
            itemSlots.Add(slot);

            slot.Initialize(this);
            slot.SetUI(item);
        }

        public void UpdateItemSlot(ItemInstance item)
        {
            InventorySlot slot = itemSlots.First(slot => slot.ItemInstance == item);

            if (slot == null) return;

            slot.SetUI(item);
        }

        public void SelectItem(InventorySlot slot)
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

            switch (item.ItemData.type)
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

            SFXManager.PlayClip(ConsumableData.usageSoundClip, player.transform.position);

            if (selectedItem.ItemInstance.Use() == false)
            {
                itemSlots.Remove(selectedItem);
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
            SFXManager.PlayClip(selectedItem.ItemInstance.ItemData.usageSoundClip, player.transform.position);
        }
        public void OnDropButton()
        {
            if (selectedItem == null) return;

            if (selectedItem.ItemInstance.equipped)
                return;

            Drop(selectedItem.ItemInstance);

            itemSlots.Remove(selectedItem);
            player.Inventory.RemoveItem(selectedItem.ItemInstance);
            Destroy(selectedItem.gameObject);

            UpdateButtons(null);
        }

        void Drop(ItemInstance itemInstance)
        {
            ItemData itemData = itemInstance.ItemData;
            Vector3 dropPosition = player.transform.position + player.transform.forward * 1.5f + player.transform.up * 1.5f;

            GameObject go = Instantiate(itemData.dropPrefab, dropPosition, Quaternion.identity);
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            rigidbody.AddForce(player.transform.forward * 2, ForceMode.Impulse);

            ItemObject itemObject = go.GetComponent<ItemObject>();
            itemObject.amount = itemInstance.amount;
            if (itemObject.itemData == null)
                itemObject.itemData = itemInstance.ItemData;
        }
    }
}
