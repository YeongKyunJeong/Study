using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CanvasUIManager : MonoSingleton<CanvasUIManager>
    {
        private GameManager gameManager;
        private Player player;
        private CombatSystemForPlayer combatSystem;
        [field: SerializeField] private UIInputReader uiInputReader;
        [field: SerializeField] private InventoryUI inventoryUI;
        public InventoryUI InventoryUI { get => inventoryUI; }

        [field: SerializeField] private PlayerInfoUI playerInfoUI;
        public PlayerInfoUI PlayerInfoUI { get => playerInfoUI; }
        [field: SerializeField] private InteractionUI interactionUI;
        public InteractionUI InteractionUI { get => interactionUI; }


        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            player = _gameManager.Player;
            combatSystem = player.CombatSystem;

            if (uiInputReader == null)
            {
                Debug.Log("UI Input Reader Not Imported");
                uiInputReader = GetComponent<UIInputReader>();
            }
            if (playerInfoUI == null)
            {
                Debug.Log("Player Info UI Not Imported");
                playerInfoUI = GetComponent<PlayerInfoUI>();
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

            uiInputReader.Initialize(gameManager);
            uiInputReader.InventoryEvent += OpenInvetoryUI;
            inventoryUI.InitializeUI(gameManager, this);

            interactionUI.Initialize(gameManager, this);
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

        public void AddInteractionButton(NPC npc,NPCInteraction newNPCInteraction)
        {
            interactionUI.AddNPCInteraction(npc, newNPCInteraction);
        }

        public void RemoveInteractionButton(NPC npc)
        {
            interactionUI.RemoveNPCInteraction(npc);

        }
    }
}
