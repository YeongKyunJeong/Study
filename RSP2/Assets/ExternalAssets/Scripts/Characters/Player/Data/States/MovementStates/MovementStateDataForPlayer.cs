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


        [field: Header("Dashing On Land")]

        [field: SerializeField] [field: Range(5f, 30f)] public float DashingSpeedModifier { get; private set; } = 10f;
        [field: SerializeField] [field: Range(0.5f, 1f)] public float DashingEndSpeedModifier { get; private set; } = 0.5f;
        [field: SerializeField] [field: Range(0f, 1f)] public float DashingLerpModifier { get; private set; } = 0.2f;
        [field: SerializeField] [field: Range(0.1f, 2f)] public float DashingDurationTime { get; private set; } = 4f;
        [field: SerializeField] [field: Range(0, 1)] public float DashFallingDelay { get; private set; } = 0.5f;
        [field: SerializeField] [field: Range(0, 1)] public float DashFallingModifier { get; private set; } = 0.5f;

    }
}
