using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class InventoryUI : MonoBehaviour, IPointerUpHandler
    {
        private GameManager gameManager;
        private CanvasUIManager uIManager;
        private Player player;


        [field: SerializeField] private GameObject itemSlotPrefab;
        [field: SerializeField] private Transform contentRoot;


        [field: SerializeField] private InventorySlot[] inventorySlots;
        [field: SerializeField] private InventorySlot selectedItemInSlot;
        [field: SerializeField] private MovingSlot movingSlot; 

        [field: SerializeField] private Button equipButton;
        [field: SerializeField] private Button useButton;
        [field: SerializeField] private Button dropButton;

        private bool isDragging;

        public void Initialize(GameManager _gameManager, CanvasUIManager _uIManager)
        {
            uIManager = _uIManager;
            player = _gameManager.Player;

            gameObject.SetActive(false);

            equipButton.onClick.AddListener(OnEquipButton);
            useButton.onClick.AddListener(OnUseButton);
            dropButton.onClick.AddListener(OnDropButton);

            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
                slot.DragBeginEvent += OnBeginSlotDrag;
                slot.ClickEvent += OnSlotClick;
                slot.PointerUpEvent += OnSlotPointerUp;
            }
            movingSlot.Initialize(this);

            isDragging = false;
        }

        public void Open()
        {
            selectedItemInSlot = null;
            UpdateButtons(null);
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void AddItemSlot(ItemInstance item)
        {
            //GameObject go = Instantiate(itemSlotPrefab, contentRoot);
            // TO DO :: Add object pooling logic to add new item 
            //MovingSlot itemInSlot = go.GetComponent<MovingSlot>();

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].ItemInstance == null)
                {
                    InventorySlot slot = inventorySlots[i];
                    slot.Initialize(this);
                    slot.SetItem(item);
                    break;
                }
            }

            return;

        }

        public void PutItemInSlot(ItemInstance item)
        {
            InventorySlot slot = inventorySlots.First(slot => slot.ItemInstance == item);

            if (slot == null) return;

            slot.SetItem(item);
        }

        public void SelectItem(InventorySlot slot)
        {
            selectedItemInSlot = slot;
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
            if (selectedItemInSlot == null) return;

            ConsumableData ConsumableData = selectedItemInSlot.ItemInstance.ItemData as ConsumableData;

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

            if (selectedItemInSlot.ItemInstance.Use() == false)
            {
                selectedItemInSlot.ClearSlot(true);
                Destroy(selectedItemInSlot.gameObject);
                selectedItemInSlot = null;
                UpdateButtons(null);
            }
            else
            {
                selectedItemInSlot.SetItem(selectedItemInSlot.ItemInstance);
            }
        }

        public void OnEquipButton()
        {
            if (selectedItemInSlot == null) return;

            player.EquipItem(selectedItemInSlot.ItemInstance);
            SFXManager.PlayClip(selectedItemInSlot.ItemInstance.ItemData.UsageSoundClip, player.transform.position);
        }

        public void OnDropButton()
        {
            if (selectedItemInSlot == null) return;

            if (selectedItemInSlot.ItemInstance.equipped)
                return;

            Drop(selectedItemInSlot.ItemInstance);

            selectedItemInSlot.ClearSlot(true);
            player.Inventory.RemoveItem(selectedItemInSlot.ItemInstance);

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

        private void OnSlotClick(InventorySlot clickedSlot, bool isSelectedBefore)
        {
            if (isSelectedBefore)
            {
                selectedItemInSlot = null;
                UpdateButtons(null);
                return;
            }

            selectedItemInSlot?.SetActiveOfSelectedFram(false);

            selectedItemInSlot = clickedSlot;
            UpdateButtons(selectedItemInSlot.ItemInstance);
            return;
        }

        private void OnBeginSlotDrag(InventorySlot draggedSlot)
        {
            if(selectedItemInSlot != null)
            {
                selectedItemInSlot.SetActiveOfSelectedFram(false);
            }
            movingSlot.CarryItem(draggedSlot);
            isDragging = true;
        }

        private void OnSlotPointerUp(InventorySlot targetSlot)
        {
            // TO DO ::
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isDragging)
            {
                // TO DO:: Add RayCastEvent
                selectedItemInSlot = null;
                isDragging = false;
                movingSlot.DropItem();
            }
        }
    }
}
