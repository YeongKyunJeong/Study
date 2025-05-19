using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum AttackType
    {
        Basic,
        MeleeAttackSkill
    }      

    public enum DetectionType
    {
        SphereCollider,
        BoxCollider
    }

    public enum MomentumDampingMode
    {
        DefaultDamping,
        SoftDamping,
        HardDamping,
        NoDamping,
        InstantStop
    }

    [System.Serializable]
    public struct ForceWithTime
    {
        public MomentumDampingMode MomentumDamping;
        public float NormalizedTime;
        public Vector3 Force;
    }

    [CreateAssetMenu(fileName = "Attack", menuName = "Custom/New Attack or Skill")]

    public class AttackData : ScriptableObject
    {
        [field: Header("Basic Attack Data Setting")]
        [field: SerializeField] public int ID;
        [field: SerializeField] public AttackType AttackType { get; private set; } = AttackType.Basic;
        [field: SerializeField] public string AttackName { get; private set; } = "BasicMeleeAttack";
        [field: SerializeField] public string VFXName { get; private set; }
        [field: SerializeField][Range(0, 1f)] public float VFXStartTime;
        public int AnimatorStateNameHash { get; private set; }
        [field: SerializeField] public LayerMask TargetLayerMask { get; private set; } = 1 << 9;
        //1 << LayerMask.NameToLayer("Combat Unit"); 

        [field: Header("General Parameter Data")]
        [field: SerializeField] public int MPCost { get; private set; }
        [field: SerializeField] public int StaminaCost { get; private set; }
        [field: SerializeField] public int Damage { get; private set; } = 3;
        [field: SerializeField] public int Intensity { get; private set; } = 5;

        [field: Header("Force Settings")]
        [field: SerializeField] public ForceWithTime[] SelfForces { get; private set; } 
        [field: SerializeField][field: Range(-10f, 10f)] public float PushForce { get; private set; }

        [field: Header("Time Data Setting")]
        [field: SerializeField][Range(0.1f, 10f)] public float AttackSpeed = 1f;
        [field: SerializeField][Range(0, 1f)] public float HitBoxActivationTime = 0.3f;
        [field: SerializeField][Range(0, 1f)] public float HitBoxDeactivationTime = 0.7f;
        [field: SerializeField][Range(0, 1f)] public float AttackRecoveryTime = 0.7f;

        [field: Header("Detection Settings")]
        [field: SerializeField] public DetectionType DetectionType { get; private set; } = DetectionType.SphereCollider;
        [field: SerializeField] public Vector3 ColliderSize { get; private set; } = new Vector3(0.8f, 0.8f, 0.8f);
        [field: SerializeField] public Vector3 ColliderPosition { get; private set; } = new Vector3(0, 1.2f, 1);

        public void GenerateHash()
        {
            AnimatorStateNameHash = Animator.StringToHash(AttackName);
        }
    }


}
