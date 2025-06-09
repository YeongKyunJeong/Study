using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace RSP2
{
    public class InteractionUI : MonoBehaviour
    {
        private GameManager gameManager;
        private CanvasUIManager canvasUIManager;

        private Dictionary<NPC, NPCInteraction> NPCInteractions;
        //private List<ItemInteraction>
        [field: SerializeField] private List<KeyValuePair<NPC, InteractionButton>> interactionButtons;

        public void Initialize(GameManager _gameManager, CanvasUIManager _canvasUIManager)
        {
            gameManager = _gameManager;
            canvasUIManager = _canvasUIManager;
            NPCInteractions = new Dictionary<NPC, NPCInteraction>();
            interactionButtons = new List<KeyValuePair<NPC, InteractionButton>>();
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

        public void AddNPCInteraction(NPC npc, NPCInteraction newNPCInteraction)
        {
            Activate();
            NPCInteractions.Add(npc, newNPCInteraction);
            interactionButtons.Add(new KeyValuePair<NPC, InteractionButton>(npc, new InteractionButton(newNPCInteraction)));
            // TO DO :: Add other logic
        }

        public void RemoveNPCInteraction(NPC npc)
        {
            NPCInteractions.Remove(npc);
            interactionButtons.RemoveAll(kvp => kvp.Key == npc);

            if (NPCInteractions.Count == 0)
            {
                Deactivate();
            }
            // TO DO :: Check there is other interaction left and deactivate if none
        }
    }
}
