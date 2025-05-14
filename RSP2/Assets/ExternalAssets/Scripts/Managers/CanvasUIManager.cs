using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CanvasUIManager : MonoBehaviour
    {
        private GameManager gameManager;
        private Player player;
        private CombatSystemForPlayer combatSystem;
        [field: SerializeField] private UIInputReader uiInputReader;
        [field: SerializeField] private InventoryUI inventoryUI;
        public InventoryUI InventoryUI { get => inventoryUI; }

        [field: SerializeField] private PlayerInfoUI playerInfoUI;


        public void Initialize(GameManager _gameManage)
        {
            gameManager = _gameManage;
            player = gameManager.Player;
            combatSystem = player.CombatSystem;

            if (uiInputReader == null)
            {
                uiInputReader = GetComponent<UIInputReader>();
            }
            if (playerInfoUI == null)
            {
                playerInfoUI = GetComponent<PlayerInfoUI>();
            }
            if (inventoryUI == null)
            {
                inventoryUI = GetComponentInChildren<InventoryUI>();
            }

            uiInputReader.Initialize(gameManager);
            uiInputReader.InventoryEvent += OpenInvetoryUI;
            inventoryUI.InitializeUI(gameManager, this);
        }

        private void Start()
        {
            player.CombatSystem.DamageEvent += ChangeHPBar;
            player.CombatSystem.HealEvent += ChangeHPBar;
            player.CombatSystem.MPRecoveryEvent += ChangeMPBar;
            player.CombatSystem.MPSpendEvent += ChangeMPBar;
            player.CombatSystem.StaminaRecoveryEvent += ChangeStaminaBar;
            player.CombatSystem.StaminaSpendEvent += ChangeStaminaBar;
        }

        public void OpenInvetoryUI()
        {
            inventoryUI.Open();

            bool isActive = inventoryUI.gameObject.activeSelf;
            gameManager.OnInventoryUIOpen(isActive);
        }

        public void ChangeHPBar()
        {
            playerInfoUI.UpdateHPUI(player.CombatSystem.CurrentHP / player.CombatSystem.MaxHP);
        }

        public void ChangeMPBar()
        {
            playerInfoUI.UpdateMPUI(player.CombatSystem.CurrentMP / player.CombatSystem.MaxMP);
        }

        public void ChangeStaminaBar()
        {
            playerInfoUI.UpdateStaminaUI(player.CombatSystem.CurrentStamina / player.CombatSystem.MaxStamina);
        }

    }
}
