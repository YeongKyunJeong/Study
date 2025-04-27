using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class UIInputReader : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;
        public event Action InventoryEvent;

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            canvasUIManager = GetComponent<CanvasUIManager>();
        }

        public void OnInventory()
        {
            InventoryEvent?.Invoke();
            //Debug.Log("Inventory");
        }
    }
}
