using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class MovementStateDataForPlayer
    {
        [field: SerializeField] [field: Range(0f, 25f)] public float WalkingSpeedModifier { get; private set; } = 3f;
        [field: SerializeField] [field: Range(0f, 25f)] public float RunningSpeedModifier { get; private set; } = 5f;
        [field: SerializeField] [field: Range(0f, 25f)] public float RotationSpeedModifier { get; private set; } = 8f;
        [field: SerializeField] [field: Range(0f, 25f)] public float JumpForceModifier { get; private set; } = 8f;
    }
}
