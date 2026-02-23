using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RSP2
{
    public class RuntimeDataForEnemy
    {
        public Vector3 HorizontalMovementVector { get; set; }
        public Vector3 VerticalVelocityVector { get; set; }
        public Vector3 AttackPositionModifier { get; set; }


        public float SearchingDistance { get; set; }
        public float SearchingDistanceSqr { get; set; }
        public float MinChasingDistance { get; set; }
        public float MinChasingDistanceSqr { get; set; }

        public LayerMask SearchingLayerMask { get; set; }
        public float FieldOfView { get; set; }
        public ChasingTargetType ChasingTargetType { get; set; }

        public float RotationSpeedModifier { get; set; }
        public float[] AttackRanges;
        public float[] AttackRangeSqrs;
        public float[] AttackAngles;
        public int[] ValidAttackIndicesBuffer;
        public int attackCount;

        public bool IsAttackReady { get; set; }
        public float RestAttackCoolTime { get; set; }

        protected bool isHostile;
        public bool IsHostile
        {
            get { return isHostile; }
            set
            {
                isHostile = value;
                // isChasingStartEvent?.Invoke();
            }
        }
        public CombatSystem Target { get; set; }

        public event Action isChasingStartEvent;

        public RuntimeDataForEnemy(int _attackCount = 0)
        {
            RotationSpeedModifier = 6;
            IsAttackReady = true;
            attackCount = _attackCount;
            AttackRanges = new float[_attackCount];
            AttackRangeSqrs = new float[_attackCount];
            AttackAngles = new float[_attackCount];
            ValidAttackIndicesBuffer = new int[_attackCount];

        }

        public void SetCoolTime(float coolTime)
        {
            IsAttackReady = false;
            RestAttackCoolTime = coolTime;
        }

        public void ResetCoolTime()
        {
            IsAttackReady = true;
            RestAttackCoolTime = 0;
        }


    }
}
