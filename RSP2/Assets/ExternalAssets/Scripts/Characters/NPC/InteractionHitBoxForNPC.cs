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
        NPCInteraction interaction;

        public void Initialize(bool[] initInteractions)
        {
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
        }

        public void OnInteractExit(Player player)
        {
            // TO DO :: Add ui off logic
        }
    }
}
