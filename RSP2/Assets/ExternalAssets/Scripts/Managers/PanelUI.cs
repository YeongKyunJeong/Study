using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSP2
{
    public class PanelUI : MonoBehaviour, IDropHandler
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;

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
