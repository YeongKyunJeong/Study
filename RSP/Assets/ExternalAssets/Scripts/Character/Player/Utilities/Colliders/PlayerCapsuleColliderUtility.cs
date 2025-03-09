using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    // This class is made to use CapsuleColliderUtility which is used for not only player
    // with data only used for player like PlayerTriggerColliderData

    [System.Serializable]
    public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }
    }
}
