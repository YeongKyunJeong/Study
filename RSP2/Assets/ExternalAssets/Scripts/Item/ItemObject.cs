using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public interface IInteractable
    {
        string GetInteractMsg();
        void OnInteract(Player player);
    }



    public class ItemObject : MonoBehaviour, IInteractable
    {
        public ItemData itemData;
        public int amount = 1;

        public string GetInteractMsg()
        {
            if (itemData == null)
                return "Pickup Unknown";
            else
                return string.Format("Pickup {0} {1}", itemData.displayName, amount);
        }

        public void OnInteract(Player player)
        {
            if (player.AddItem(itemData, amount))
                Destroy(gameObject);
        }
    }
}
