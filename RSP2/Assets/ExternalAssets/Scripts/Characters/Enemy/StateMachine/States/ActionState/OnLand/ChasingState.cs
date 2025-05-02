using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ChasingState : OnLandStateForEnemy
    {
        private readonly int isChasingHash = Animator.StringToHash("IsChasing");
        private readonly int instantChasingHash = Animator.StringToHash("OnLand.Chasing");

        private float distance;

        public ChasingState(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);

            moveDir = Vector3.zero;
            mover.UpdateNextHorizontalMovementVector(moveDir);
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantChasingHash, 0.25f);
            }

        }
        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }


        public override void CallUpdate()
        {
            base.CallUpdate();

            //if (IsInAttackRange())
            //{
            //    if (IsInSight())
            //    {
            //        stateMachine.ChangeState(stateMachine.BasicAttackingState);
            //        return;
            //    }
            //}

            if (enemy.Target == null)
            {
                if (!SearchForTaget())
                {
                    stateMachine.ChangeState(stateMachine.IdlingState);
                    return;
                }
            }
            else
            {

                //float distance = Vector3.Distance(enemy.Target.transform.position, enemy.transform.position);
                if (TargetDistanceSqr >= enemy.SearchingDistanceSqr * 1.2f)
                //if (distance >= enemy.SearchingDistance * 1.2f)
                {
                    SearchForTaget();
                    if (enemy.Target == null)
                    {
                        stateMachine.ChangeState(stateMachine.IdlingState);
                    }
                    return;
                }

                if(TargetDistanceSqr <= enemy.AttackRangeSqr)
                {
                    if (IsInSight()) 
                    {
                        SetAnimatorOnLandParameter(false);
                        stateMachine.ChangeState(stateMachine.AttackingState);
                        return;
                    }
                }


                //moveDir = (enemy.Target.transform.position - enemy.transform.position);
                moveDir = TargetVector;
                moveDir.y = 0;
                //moveDir = moveDir.normalized * enemy.ChasingSpeedModifier;
                moveDir = moveDir.normalized * statisticsHandler.CurrentStatistics.MovementSpeed;
                mover.UpdateNextHorizontalMovementVector(moveDir);
            }

        }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantChasingHash, 0.25f);
            }
            animator.SetBool(isChasingHash, isOn);
        }
    }
}
