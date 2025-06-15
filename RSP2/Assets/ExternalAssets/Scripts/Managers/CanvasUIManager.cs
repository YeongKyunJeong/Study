using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    public class CanvasUIManager : MonoSingleton<CanvasUIManager>
    {
        private GameManager gameManager;
        private Player player;
        private CombatSystemForPlayer combatSystem;

        //[field: SerializeField] private UIInputReader uiInputReader;

        [field: SerializeField] public FixedUI FixedUI { get; private set; }
        [field: SerializeField] public PanelUI PanelUI { get; private set; }
        [field: SerializeField] public PopUpUI PopUpUI { get; private set; }

        public bool IsInventoryOpened { get; private set; }

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            player = _gameManager.Player;
            combatSystem = player.CombatSystem;

            player.InputReader.InventoryEvent += OpenInventoryUI;


            if (FixedUI == null)
            {
                Debug.Log("Fixed UI Not Imported");
                FixedUI = GetComponent<FixedUI>();
            }
            FixedUI.Initialize(gameManager, this);

            if (PanelUI == null)
            {
                Debug.Log("Panel UI Not Imported");
                PanelUI = GetComponent<PanelUI>();
            }
            PanelUI.Initialize(gameManager, this);

            if (PopUpUI == null)
            {
                Debug.Log("Panel UI Not Imported");
                PopUpUI = GetComponent<PopUpUI>();
            }
            PopUpUI.Initialize(gameManager, this);

            IsInventoryOpened = false;

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

        #region Fixed UI Methods

        public void ChangeHPBar()
        {
            FixedUI.PlayerInfoUI.UpdateHPUI(player.CombatSystem.CurrentHP / player.CombatSystem.MaxHP);
        }

        public void ChangeMPBar()
        {
            FixedUI.PlayerInfoUI.UpdateMPUI(player.CombatSystem.CurrentMP / player.CombatSystem.MaxMP);
        }

        public void ChangeStaminaBar()
        {
            FixedUI.PlayerInfoUI.UpdateStaminaUI(player.CombatSystem.CurrentStamina / player.CombatSystem.MaxStamina);
        }
        #endregion


        #region Panel UI Methods

        public void OpenInventoryUI()
        {
            PanelUI.OpenInventoryUI();

            bool isActive = PanelUI.InventoryUI.gameObject.activeSelf;
            IsInventoryOpened = isActive;
            gameManager.OnInventoryUIOpen(isActive);
        }


        //public void AddInteractionButton(NPC npc, NPCInteraction newNPCInteraction)
        //{
        //    PanelUI.InteractionUI.AddNPCInteraction(npc, newNPCInteraction);
        //}

        //public void RemoveInteractionButton(NPC npc)
        //{
        //    PanelUI.InteractionUI.RemoveNPCInteraction(npc);
        //}
        #endregion


        #region Pop Up UI Methods

        #endregion
    }
}
