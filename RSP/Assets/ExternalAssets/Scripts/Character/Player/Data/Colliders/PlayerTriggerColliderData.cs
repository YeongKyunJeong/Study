using UnityEngine;

namespace RSP
{
    [System.Serializable]
    public class PlayerTriggerColliderData
    {
        [field:SerializeField] public BoxCollider GroundCheckCollider { get; private set; }
    }
}
