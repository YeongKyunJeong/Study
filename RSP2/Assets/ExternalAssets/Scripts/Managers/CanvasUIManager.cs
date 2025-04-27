using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CanvasUIManager : MonoBehaviour
    {
        private GameManager gameManager;
        private UIInputReader uiInputReader;
        private InventoryUI inventoryUI;

        public InventoryUI InventoryUI { get => inventoryUI; }

        public void Initialize(GameManager _gameManage)
        {
            gameManager = _gameManage;
            uiInputReader = GetComponent<UIInputReader>();
            inventoryUI = GetComponentInChildren<InventoryUI>();

            uiInputReader.Initialize(gameManager);
            uiInputReader.InventoryEvent += OpenInvetoryUI;
            inventoryUI.InitializeUI(gameManager, this);
        }

        public void OpenInvetoryUI()
        {
            inventoryUI.Open();

            bool isActive = inventoryUI.gameObject.activeSelf;
            gameManager.OnInventoryUIOpen(isActive);

        }
    }
}
