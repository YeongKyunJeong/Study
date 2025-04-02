using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackStateForPlayer : MovementStateForPlayer
    {
        protected readonly int attackingHash = Animator.StringToHash("isAttacking");
        protected readonly int attackableHash = Animator.StringToHash("Attackable");
        protected readonly int attackTriggerHash = Animator.StringToHash("Attack");

        protected string animatorAttackStateTag = "Attack State";

        protected Vector3 horizontalMomentum;
        protected float passedTime;

        protected float attackAnimationTime;

        public AttackStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorIsAttacking(true);
            SetAnimatorIsAttack();

            animator.speed *= attackStateData.AttackSpeedMultiplier;
            attackAnimationTime = GetAnimationLength(animator, animatorAttackStateTag);
            if (attackAnimationTime < 0)
            {
                Debug.Log("not work properly");
                Debug.Log(attackAnimationTime);
            }
            else
            {
                Debug.Log(attackAnimationTime);
            }
            passedTime = 0;
        }

        public override void Exit()
        {
            base.Exit();

            animator.speed = 1;

            SetAnimatorIsAttacking(false);
        }



        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

        }

        protected virtual void SetAnimatorAttackable(bool isOn)
        {
            animator.SetBool(attackableHash, isOn);

        }

        protected virtual void SetAnimatorIsAttacking(bool isOn)
        {
            animator.SetBool(attackingHash, isOn);
        }
        protected virtual void SetAnimatorIsAttack()
        {
            animator.SetTrigger(attackTriggerHash);
        }

    }





}
