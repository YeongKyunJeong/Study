using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RSP2
{
    [System.Serializable]

    public class AttackStateDataForPlayer
    {
        [field: SerializeField] public AttackData BaseAttackData { get; private set; }
        [field: SerializeField] public List<AttackData> AttackDataList { get; private set; }

        //public AttackData GetSkillInfo(int slotIndex)
        //{
        //    if (SkillIndexSlot.Count <= slotIndex) return null;

        //    int skillID = SkillIndexSlot[slotIndex];
        //    return AttackInfoDatas.Count <= skillID ? null : AttackInfoDatas[skillID];
        //}

        public AttackData GetAttackInfo(int index)
        {
            return AttackDataList.Count <= index ? null : AttackDataList[index];
        }

        public void Initialize()
        {
            BaseAttackData.GenerateHash();
            foreach (var attackInfo in AttackDataList)
            {
                attackInfo.GenerateHash();
            }
        }
    }

    public enum AttackType
    {
        Basic,
        Combo
    }

    public enum DetectionType
    {
        SphereCollider,
        BoxCollider
    }

    [Serializable]
    public class AttackData
    {
        [field: Header("Basic Attack Data Setting")]
        [field: SerializeField] public AttackType AttackType { get; private set; } = AttackType.Basic;
        [field: SerializeField] public string AttackName { get; private set; } = "BasicMeleeAttack";
        public int AnimatorStateNameHash { get; private set; }
        [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
        //1 << LayerMask.NameToLayer("Combat Unit");

        [field: Header("General Parameter Data")]
        [field: SerializeField] public int Damage { get; private set; } = 3;
        [field: SerializeField] public int Intensity = 5;

        [field: Header("Force Settings")]
        [field: SerializeField][field: Range(0f, 1f)] public float ForceTransitionTime { get; private set; }
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

        [field: Header("Attack Movement Data")]
        [field: SerializeField][Range(0, 15f)] public float MovementDamping = 5f;
        public void GenerateHash()
        {
            AnimatorStateNameHash = Animator.StringToHash(AttackName);
        }
    }
}
