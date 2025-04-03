using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackStateForPlayer : MovementStateForPlayer
    {
        protected string animatorAttackStateTag = "Attack State";

        protected Vector3 horizontalMomentum;
        protected float passedTime;

        protected float attackMinimumDuration;
        protected float attackAnimationTime;

        public AttackStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorIsAttackingParameter(true);

            animator.speed *= attackStateData.AttackSpeedMultiplier;

            horizontalMomentum = runtimeData.HorizontalMovementVector;

            //attackAnimationTime = GetAnimationLength(animator, animatorAttackStateTag);
            //if (attackAnimationTime < 0)
            //{
            //    Debug.Log("not work properly");
            //    Debug.Log(attackAnimationTime);
            //}
            //else
            //{
            //    Debug.Log(attackAnimationTime);
            //}
            passedTime = 0;
        }


        public override void Exit()
        {
            base.Exit();

            animator.speed = 1;

            SetAnimatorIsAttackingParameter(false);
        }



        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

        }

    }





}
