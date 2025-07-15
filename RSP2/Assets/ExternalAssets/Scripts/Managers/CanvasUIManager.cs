using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public enum PanelUIType
    {
        Inventory,
        Interaction,
        Dialogue
    }

    public class CanvasUIManager : MonoSingleton<CanvasUIManager>
    {
        private GameManager gameManager;
        private InteractionManager interactionManager;
        private Player player;
        private CombatSystemForPlayer combatSystem;
        private bool isOnDialogue { get; set; }
        public event Action<int> dialogueEndEvent;

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
            gameManager.Player.InputReader.ClickWhileInteractionEvent += OnNextInput;
            //gameManager.Player.InputReader.On

            player.CombatSystem.DamageEvent += ChangeHPBar;
            player.CombatSystem.HealEvent += ChangeHPBar;
            player.CombatSystem.MPRecoveryEvent += ChangeMPBar;
            player.CombatSystem.MPSpendEvent += ChangeMPBar;
            player.CombatSystem.StaminaRecoveryEvent += ChangeStaminaBar;
            player.CombatSystem.StaminaSpendEvent += ChangeStaminaBar;
        }

        public void SetPanelUIActive(PanelUIType uIType, bool isOn = true, int count = 1)
        {
            switch (uIType)
            {
                case PanelUIType.Inventory:
                    {
                        PanelUI.OpenInventoryUI();

                        bool isActive = PanelUI.InventoryUI.gameObject.activeSelf;
                        IsInventoryOpened = isActive;
                        gameManager.OnInventoryUIOpen(isActive);
                        break;
                    }
                case PanelUIType.Interaction:
                    {
                        if (isOn) PanelUI.InteractionUI.Activate(count);
                        else PanelUI.InteractionUI.Deactivate(count);
                        break;
                    }
                case PanelUIType.Dialogue:
                    {
                        if (isOn) PanelUI.DialogueUI.Activate();
                        else PanelUI.DialogueUI.Deactivate();
                        break;
                    }
            }

        }

        #region Fixed UI Methods

        public void ChangeHPBar(float leftHP, float maxHP)
        {
            FixedUI.PlayerInfoUI.UpdateHPUI(leftHP / maxHP);
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

        #region Dialogue
        public void SendDialogueStartCall(DialogueType dialogueType, int key)
        {
            isOnDialogue = true;
            PanelUI.DialogueUI.StartDialogue(dialogueType, key);
        }

        public void SendInteractionUITMPChangeCall(string targetName, string interactionName, int count)
        {
            PanelUI.InteractionUI.ChangeInteractionDisplayTMP(targetName, interactionName, count);
        }

        public void OnNextInput()
        {
            if (isOnDialogue)
            {
                int next = PanelUI.DialogueUI.Next();
                if (next >= 0)
                {
                    PanelUI.DialogueUI.Deactivate();
                    isOnDialogue = false;
                    dialogueEndEvent?.Invoke(next);
                }
            }
        }
        #endregion

        private void OpenInventoryUI()
        {
            SetPanelUIActive(PanelUIType.Inventory);
        }
        #endregion


        #region Pop Up UI Methods

        #endregion
    }
}
