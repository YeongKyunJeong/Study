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
        private Player player;

        [field: SerializeField] private InteractionStringTable InteractionStringSO { get; set; }

        [field: SerializeField] private InteractionUI interactionUI;
        [field: SerializeField] private InteractionDisplay interactionDisplay;

        //private Dictionary<NPC, NPCInteraction> NPCInteractions;
        [field: SerializeField] public bool isInteractable;
        [field: SerializeField] public bool isInteracting;

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


        public void Initialize(GameManager _gameManager, CameraManager _cameraManager)
        {
            gameManager = _gameManager;
            cameraManager = _cameraManager;
            //NPCInteractions = new Dictionary<NPC, NPCInteraction>();
            interactionPairList = new List<KeyValuePair<NPC, NPCInteraction>>();
        }

        private void Start()
        {
            player = gameManager.Player;
            player.InputReader.InteractionEvent += OnInteractionInput;

            isInteractable = CheckIsInteractable();
            isInteracting = false;
        }

        private bool CheckIsInteractable()
        {
            // TO DO :: Add checking logic whether is interactable
            return true;
        }

        public void AddNPCInteraction(NPC nPC, NPCInteraction newNPCInteraction)
        {
            InteractionUI.Activate();
            //NPCInteractions.Add(nPC, newNPCInteraction);
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
                InteractionUI.Deactivate();
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
            interactionUI.ChangeInteractionDisplayTMP(nextPair.Key, GetInteractionName(nextPair.Value));
        }


        public string GetInteractionName(NPCInteraction nPCInteraction)
        {
            return InteractionStringSO.GetString(nPCInteraction);
        }

        private void OnInteractionInput()
        {
            if (isInteracting)
            {
                WhileInteraction();
                return;
            }

            if (!isInteractable) return;

            if (currentPair.Key == null) return;

            // TO DO:: Start Interaction by interaction type
            isInteracting = true;
            cameraManager.CallCameraSwitching(currentPair.Key.nPCCamera.VirtualCamera);
        }

        private void WhileInteraction()
        {
            // TO DO :: Add interaction input logic while interaction 
            // Temporary
            isInteracting = false;
            cameraManager.CallCameraSwitching(null);
        }

    }
}
