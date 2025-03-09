using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    [System.Serializable]
    public class PlayerAirborneData
    {
        [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
        [field: SerializeField] public PlayerFallData FallData { get; private set; }
    }
}
