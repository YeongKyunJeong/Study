using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public enum InteractionType
    {
        Oven,
        Stall_Player,
        Stall_Customer
    }

    public class InteractionManager : MonoBehaviour
    {
        [field: SerializeField] private List<Interactable> interactableObjects;
        private GameManager gameManager;
        private BreadManager breadManager;

        public void Initialize() 
        {
            gameManager = GameManager.Instance;
            breadManager = gameManager.BreadManage;

            foreach (Interactable interactable in interactableObjects)
            {
                interactable.Initialize(this);
            }
        }

        public void CallUpdate()
        {
            foreach (Interactable interactable in interactableObjects)
            {
                interactable.Detect();
            }
        }

        public void OnInteraction(InteractionType interactionType) 
        {
            switch (interactionType) 
            {
                case InteractionType.Oven:
                    {
                        breadManager.MoveBreadToTray();

                        break;
                    }
                case InteractionType.Stall_Player:
                    {
                        breadManager.MoveBreadToStall();

                        break;
                    }
            }
        }
    }
}
