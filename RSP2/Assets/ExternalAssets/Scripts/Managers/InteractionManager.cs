using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    public class InteractionManager : MonoSingleton<InteractionManager>
    {
        private GameManager gameManager;
        private CameraManager cameraManager;
        private CanvasUIManager canvasUIManager;
        private Player player;
        private ActionStateMachineForPlayer playerStateMachine;

        [field: SerializeField] private InteractionStringTable InteractionStringSO { get; set; }

        [field: SerializeField] private InteractionUI interactionUI;
        [field: SerializeField] private InteractionDisplay interactionDisplay;

        //private Dictionary<NPC, NPCInteraction> NPCInteractions;
        [field: SerializeField] private bool isInteracting;

        [field: SerializeField] private List<KeyValuePair<NPC, NPCInteraction>> interactionPairList;
        [field: SerializeField] private KeyValuePair<NPC, NPCInteraction> currentPair;

        public InteractionUI InteractionUI
        {
            get
            {
                if (interactionUI == null)
                {
                    interactionUI = FindObjectOfType<InteractionUI>();
                    if (!interactionUI)
                    {
                        Debug.LogError("Interaction UI Not Detected");
                        return null;
                    }
                }

                return interactionUI;
            }
        }


        public void Initialize(GameManager _gameManager, CameraManager _cameraManager, CanvasUIManager _canvasUIManager)
        {
            gameManager = _gameManager;
            cameraManager = _cameraManager;
            canvasUIManager = _canvasUIManager;
            //NPCInteractions = new Dictionary<NPC, NPCInteraction>();
            canvasUIManager.dialogueEndEvent += OnDialogueEnd;
            interactionPairList = new List<KeyValuePair<NPC, NPCInteraction>>();
            isInteracting = false;
        }

        private void Start()
        {
            //canvasUIManager.PanelUI.DialogueUI.

            player = gameManager.Player;
            playerStateMachine = player.ActionStateMachine;
            player.InputReader.InteractionEvent += OnInteractionInput;

            //isInteractable = CheckIsInteractable();
        }

        private bool CheckIsInteractable()
        {
            // TO DO :: Add checking logic whether is interactable
            if (!playerStateMachine.isOnLand) return false;

            if (canvasUIManager.IsInventoryOpened) return false;

            if (playerStateMachine.isDead) return false;

            return true;
        }

        public void AddNPCInteraction(NPC nPC, NPCInteraction newNPCInteraction)
        {
            canvasUIManager.SetPanelUIActive(PanelUIType.Interaction, true);
            KeyValuePair<NPC, NPCInteraction> newPair = new KeyValuePair<NPC, NPCInteraction>(nPC, newNPCInteraction);
            interactionPairList.Add(newPair);
            ChangeCurrentInteraction(newPair);
            // TO DO :: Add other logic
        }

        public void RemoveNPCInteraction(NPC nPC)
        {
            interactionPairList.RemoveAll(kvp => kvp.Key == nPC);

            if (interactionPairList.Count == 0)
            {
                canvasUIManager.SetPanelUIActive(PanelUIType.Interaction, false);
                currentPair = new KeyValuePair<NPC, NPCInteraction>();
            }
            else
            {
                if (interactionPairList.Contains(currentPair)) return;

                int index = interactionPairList.Count - 1;
                ChangeCurrentInteraction(interactionPairList[index]);
            }
            // TO DO :: Check there is other interaction left and deactivate if none
        }

        public void ChangeCurrentInteraction(KeyValuePair<NPC, NPCInteraction> nextPair)
        {
            currentPair = nextPair;
            canvasUIManager.SendInteractionUITMPChangeCall(nextPair.Key.Name, GetInteractionName(nextPair.Value));
            //interactionUI.ChangeInteractionDisplayTMP(nextPair.Key, GetInteractionName(nextPair.Value));
        }


        public string GetInteractionName(NPCInteraction nPCInteraction)
        {
            return InteractionStringSO.GetString(nPCInteraction);
        }

        private void OnInteractionInput()
        {
            if (isInteracting) return;

            if (!CheckIsInteractable()) return;

            if (currentPair.Key == null) return;

            StartInteraction();
        }

        private void StartInteraction()
        {
            isInteracting = true;
            canvasUIManager.SetPanelUIActive(PanelUIType.Interaction, false);
            canvasUIManager.SetPanelUIActive(PanelUIType.Dialogue, true);
            cameraManager.CallCameraSwitching(currentPair.Key.NPCCamera.VirtualCamera);
            gameManager.OnInteractionUIOpen(true);


            switch (currentPair.Value)
            {
                case NPCInteraction.Speakable:
                    canvasUIManager.SendDialogueStartCall(DialogueType.NPC, currentPair.Key.DialogueKey);
                    break;
                case NPCInteraction.Tradable:
                    break;
                default:
                    break;
            }
        }

        private void EndCurrentInteraction()
        {
            switch (currentPair.Value)
            {
                case NPCInteraction.Speakable:
                    {
                        canvasUIManager.SetPanelUIActive(PanelUIType.Interaction, true);
                        canvasUIManager.SetPanelUIActive(PanelUIType.Dialogue, false);
                        cameraManager.CallCameraSwitching(null);
                        gameManager.OnInteractionUIOpen(false);

                        break;
                    }
            }

            isInteracting = false;
        }

        private void OnDialogueEnd(int redirection)
        {
            currentPair.Key.DialogueKey = redirection;
            EndCurrentInteraction();
        }
    }
}
