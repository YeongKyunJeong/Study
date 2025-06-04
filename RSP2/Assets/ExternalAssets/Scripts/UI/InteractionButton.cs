using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RSP2
{
    public class InteractionButton : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI buttonKey { get; set; }
        [field: SerializeField] private TextMeshProUGUI explanation { get; set; }

        public InteractionButton(NPCInteraction nPCInteraction) // TO DO :: Add interaction button constructor
        {

        }


        //public InteractionButton(ObjectInteraction objectInteraction) // TO DO:: Add other interaction button constructor
        //{

        //}

        //public InteractionButton(ItemInteraction ItemInteraction) // To do
        //{

        //}
    }
}
