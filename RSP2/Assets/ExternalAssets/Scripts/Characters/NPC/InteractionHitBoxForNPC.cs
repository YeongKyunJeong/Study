using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    [Flags]
    public enum NPCInteraction
    {
        Speakable = 1,
        Tradable = 2,
    }

    public class InteractionHitBoxForNPC : MonoBehaviour, IInteractable
    {
        [field: SerializeField] private NPCInteraction interaction;
        [field: SerializeField] private NPC myNPC;

        public void Initialize(NPC NPC, bool[] initInteractions)
        {
            if (NPC == null)
            {
                NPC = transform.parent.GetComponent<NPC>();
            }
            else
            {
                myNPC = NPC;
            }

            interaction = 0;

            int count = Enum.GetValues(typeof(NPCInteraction)).Length;
            NPCInteraction[] newInteractions = (NPCInteraction[])Enum.GetValues(typeof(NPCInteraction));

            for (int i = 0; i < initInteractions.Length; i++)
            {
                if (i < count && initInteractions[i])
                {
                    interaction |= newInteractions[i];
                }
            }
        }

        public void AddInteraction(NPCInteraction newInteraction)
        {
            interaction |= newInteraction;
        }

        public void RemoveInteraction(NPCInteraction removedInteraction)
        {
            interaction &= ~removedInteraction;
        }

        public string GetInteractMsg()
        {
            return string.Empty;
            //if (itemData == null)
            //    return "Pickup Unknown";
            //else
            //    return string.Format("Pickup {0} {1}", itemData.displayName, amount);
        }

        public void OnInteractEnter(Player player)
        {
            // TO DO :: Add ui on logic
            NPCInteraction[] newInteractions = (NPCInteraction[])Enum.GetValues(typeof(NPCInteraction));
            foreach (var item in newInteractions)
            {
                if (interaction.HasFlag(item))
                {
                    CanvasUIManager.Instance.AddInteractionButton(myNPC, item);
                }
            }
        }

        public void OnInteractExit(Player player)
        {
            // TO DO :: Add ui off logic
            CanvasUIManager.Instance.RemoveInteractionButton(myNPC);
        }
    }
}
