using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PanelUI : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;

        [field: SerializeField] private InventoryUI inventoryUI;
        public InventoryUI InventoryUI { get => inventoryUI; }

        [field: SerializeField] private InteractionUI interactionUI;
        public InteractionUI InteractionUI { get => interactionUI; }

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

            inventoryUI.Initialize(gameManager, canvasUIManager);
            interactionUI.Initialize(gameManager, canvasUIManager);
        }

        public void OpenInventoryUI() 
        {
            inventoryUI.Open();
        }
    }
}
