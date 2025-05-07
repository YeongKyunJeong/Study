using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class InteractionHitBox : MonoBehaviour
    {
        private Player player;

        public void Initialize(Player _player)
        {
            player = _player;
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                //floatingTextManager.CreateFloatingText(interactable.GetInteractMsg(), other.transform.position);
                interactable?.OnInteract(player);
            }
        }
    }
}
