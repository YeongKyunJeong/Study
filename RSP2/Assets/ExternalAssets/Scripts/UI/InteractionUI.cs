using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace RSP2
{
    public class InteractionUI : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;

        [field: SerializeField] InteractionStringTable InteractionStringSO;

        [field: SerializeField] private InteractionDisplay interactionDisplay;
        private Dictionary<NPC, NPCInteraction> NPCInteractions;
        [field: SerializeField] private List<KeyValuePair<NPC, NPCInteraction>> CurrentInteractions;

        public void Initialize(GameManager _gameManager, CanvasUIManager _canvasUIManager)
        {
            if(InteractionStringSO == null)
            {
                Debug.LogError("Interaction String SO Not Assigned");
            }

            if(interactionDisplay == null)
            {
                Debug.LogError("Interaction Display Not Assigned");
            }

            gameManager = _gameManager;
            canvasUIManager = _canvasUIManager;
            NPCInteractions = new Dictionary<NPC, NPCInteraction>();
            CurrentInteractions = new List<KeyValuePair<NPC, NPCInteraction>>();
            Deactivate();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void AddNPCInteraction(NPC nPC, NPCInteraction newNPCInteraction)
        {
            Activate();
            NPCInteractions.Add(nPC, newNPCInteraction);
            CurrentInteractions.Add(new KeyValuePair<NPC, NPCInteraction>(nPC, newNPCInteraction));
            ChangeInteractionDisplayTMP(nPC, newNPCInteraction);
            // TO DO :: Add other logic
        }

        public void RemoveNPCInteraction(NPC nPC)
        {
            NPCInteractions.Remove(nPC);
            CurrentInteractions.RemoveAll(kvp => kvp.Key == nPC);

            if (NPCInteractions.Count == 0)
            {
                Deactivate();
            }
            // TO DO :: Check there is other interaction left and deactivate if none
        }

        public void ChangeInteractionDisplayTMP(NPC nPC, NPCInteraction nPCInteraction)
        {
            string InteractionString = InteractionStringSO.GetString(nPCInteraction);
            interactionDisplay.ChangeString(InteractionString, nPC.Name);
        }
    }
}
