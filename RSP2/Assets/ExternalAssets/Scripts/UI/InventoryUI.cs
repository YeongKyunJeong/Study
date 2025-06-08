using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RSP2
{
    public class InventoryUI : MonoBehaviour, IDropHandler
    {
        private GameManager gameManager;
        private CanvasUIManager uIManager;
        private PanelUI panelUI;
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

        public void Initialize(GameManager _gameManager, CanvasUIManager _uIManager, PanelUI _panelUI)
        {
            player = _gameManager.Player;
            uIManager = _uIManager;
            panelUI = _panelUI;
            panelUI.PointerDropEvent += OnBackGroundDrop;

            gameObject.SetActive(false);

            equipButton.onClick.AddListener(OnEquipButton);
            useButton.onClick.AddListener(OnUseButton);
            dropButton.onClick.AddListener(OnDropButton);

            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
                slot.DragBeginEvent += OnBeginSlotDrag;
                slot.ClickEvent += OnSlotClick;
                slot.PointerDropEvent += OnSlotPointerDrop;
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

        public bool AddItemToSlot(ItemInstance item)
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

                    return true;
                }
            }

            return false;

        }

        public void PutItemInSlot(ItemInstance item)
        {
            InventorySlot emptySlot = inventorySlots.First(slot => slot.ItemInstance == item);

            if (emptySlot == null) return;

            emptySlot.SetItem(item);
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

            player.Inventory.Drop(selectedItemInSlot.ItemInstance);

            player.Inventory.RemoveItem(selectedItemInSlot.ItemInstance);
            selectedItemInSlot.ClearSlot(true);

            UpdateButtons(null);
        }

        //public void Drop(ItemInstance itemInstance)
        //{
        //    ItemData itemData = itemInstance.ItemData;
        //    Vector3 dropPosition = player.transform.position + player.transform.forward * 1.5f + player.transform.up * 1.5f;

        //    GameObject go = Instantiate(itemData.DropPrefab, dropPosition, Quaternion.identity);
        //    Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        //    rigidbody.AddForce(player.transform.forward * 2, ForceMode.Impulse);

        //    ItemObject itemObject = go.GetComponent<ItemObject>();
        //    itemObject.amount = itemInstance.amount;
        //    if (itemObject.itemData == null)
        //        itemObject.itemData = itemInstance.ItemData;
        //}

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
            if (selectedItemInSlot != null && selectedItemInSlot != draggedSlot)
            {
                selectedItemInSlot.SetActiveOfSelectedFram(false);
            }

            selectedItemInSlot = draggedSlot;
            movingSlot.CarryItem(draggedSlot);
            isDragging = true;
        }

        private void OnSlotPointerDrop(InventorySlot targetSlot)
        {
            if (isDragging)
            {
                isDragging = false;

                selectedItemInSlot.SetItem(targetSlot.ItemInstance);
                targetSlot.SetItem(movingSlot.ItemInstance);
                movingSlot.DropItem();
                selectedItemInSlot = null;
                UpdateButtons(null);
            }
        }
        
        private void OnBackGroundDrop()
        {
            OnDropButton();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (isDragging)
            {
                isDragging = false;
                movingSlot.DropItem();
                selectedItemInSlot.SetActiveOfSelectedFram(true);
                UpdateButtons(selectedItemInSlot.ItemInstance);
            }
        }
    }
}
