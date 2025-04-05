using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]

    public class AttackStateDataForPlayer
    {
        [field: Header("Attack Time Data")]
        [field: SerializeField] [Range(0.1f, 10f)] public float AttackSpeedMultiplier = 1f;
        [field: SerializeField] [Range(0, 1f)] public float AttackHitBoxEnableMultiplier = 0.3f;
        [field: SerializeField] [Range(0, 1f)] public float AttackHitBoxDiableMultiplier = 0.7f;
        [field: SerializeField] [Range(0, 1f)] public float AttackRecoveryMultiplier = 0.7f; 

        [field: Header("Attack Hit Box Data")]
        [field: SerializeField] public Vector3[] AttackSizeMultiplyer = new Vector3[] { new Vector3(1, 1, 1) };
        [field: SerializeField] public Vector3[] AttackPositionMultiplyer = new Vector3[] { new Vector3(0, 0.9f, 1) };

        [field: Header("Attack Movement Data")]
        [field: SerializeField] [Range(0, 15f)] public float AttackDampingModifier = 5f;


        [field: Header("Attack Parameter Data")]
        [field: SerializeField] [Range(0, 10)] public int AttackIntensity = 5;

    }
}
