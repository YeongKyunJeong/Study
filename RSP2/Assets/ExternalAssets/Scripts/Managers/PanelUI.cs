using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSP2
{
    public class PanelUI : MonoBehaviour, IDropHandler
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;

        [field: SerializeField] private EquipmentStatsDisplay statsDisplay;
        public EquipmentStatsDisplay StatsDisplay { get => statsDisplay; }

        [field: SerializeField] private InventoryUI inventoryUI;
        public InventoryUI InventoryUI { get => inventoryUI; }

        [field: SerializeField] private InteractionUI interactionUI;
        public InteractionUI InteractionUI { get => interactionUI; }

        [field: SerializeField] private DialogueUI dialogueUI;
        public DialogueUI DialogueUI { get => dialogueUI; }

        public event Action PointerDropEvent;

        public void Initialize(GameManager _gameManager, CanvasUIManager _canvasUIManager)
        {
            gameManager = _gameManager;
            canvasUIManager = _canvasUIManager;

            if (statsDisplay == null)
            {
                Debug.Log("Stats Display Not Imported");
                statsDisplay = GetComponentInChildren<EquipmentStatsDisplay>();
            }
            if (inventoryUI == null)
            {
                Debug.Log("Inventory UI Not Imported");
                inventoryUI = GetComponentInChildren<InventoryUI>();
            }
            if (interactionUI == null)
            {
                Debug.Log("interactionUI UI Not Imported");
                interactionUI = GetComponentInChildren<InteractionUI>();
            }
            if (dialogueUI == null)
            {
                Debug.Log("dialogueUI UI Not Imported");
                dialogueUI = GetComponentInChildren<DialogueUI>();
            }

            statsDisplay.Initialize(gameManager, canvasUIManager);
            inventoryUI.Initialize(gameManager, canvasUIManager, this);
            interactionUI.Initialize(gameManager, canvasUIManager);
            dialogueUI.Initialize(gameManager);
        }


        public void OpenInventoryUI()
        {
            inventoryUI.Open();
        }

        public void OnDrop(PointerEventData eventData)
        {
            PointerDropEvent?.Invoke();
        }

    }
}
