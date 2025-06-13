using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class InteractionManager : MonoSingleton<InteractionManager>
    {
        private GameManager gameManager;
        [field: SerializeField] private InteractionUI interactionUI;

        [field: SerializeField] private InteractionDisplay interactionDisplay;
        //private Dictionary<NPC, NPCInteraction> NPCInteractions;
        [field: SerializeField] private List<KeyValuePair<NPC, NPCInteraction>> CurrentInteractions;

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

        [field: SerializeField] private InteractionStringTable InteractionStringSO { get; set; }

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            //NPCInteractions = new Dictionary<NPC, NPCInteraction>();
            CurrentInteractions = new List<KeyValuePair<NPC, NPCInteraction>>();
        }

        public void AddNPCInteraction(NPC nPC, NPCInteraction newNPCInteraction)
        {
            InteractionUI.Activate();
            //NPCInteractions.Add(nPC, newNPCInteraction);
            CurrentInteractions.Add(new KeyValuePair<NPC, NPCInteraction>(nPC, newNPCInteraction));
            ChangeInteractionDisplay(nPC, newNPCInteraction);
            // TO DO :: Add other logic
        }

        public void RemoveNPCInteraction(NPC nPC)
        {
            CurrentInteractions.RemoveAll(kvp => kvp.Key == nPC);

            if (CurrentInteractions.Count == 0)
            {
                InteractionUI.Deactivate();
            }
            else
            {
                int index = CurrentInteractions.Count - 1;
                ChangeInteractionDisplay(CurrentInteractions[index].Key, CurrentInteractions[index].Value);
            }
            // TO DO :: Check there is other interaction left and deactivate if none
        }

        public void ChangeInteractionDisplay(NPC nPC, NPCInteraction nPCInteraction)
        {
            interactionUI.ChangeInteractionDisplayTMP(nPC, GetInteractionName(nPCInteraction));
        }

        public string GetInteractionName(NPCInteraction nPCInteraction)
        {
            return InteractionStringSO.GetString(nPCInteraction);
        }

    }
}
