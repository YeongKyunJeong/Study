using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class IdlingStateForEnemy : OnLandStateForEnemy
    {
        private readonly int isIdlingHash = Animator.StringToHash("IsIdling");
        private readonly int instantIdlingHash = Animator.StringToHash("OnLand.Idling");


        public IdlingStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);

            moveDir = Vector3.zero;
            mover.UpdateNextHorizontalMovementVector(moveDir);

        }
        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        public override void CallUpdate()
        {
            if (IsInAttackRange())
            {
                if (IsInSight())
                {
                    stateMachine.ChangeState(stateMachine.AttackingState);
                }
                else
                {
                    base.CallUpdate();
                }
            }
            else if (SearchForTaget())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
            }
        }

        #endregion


        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantIdlingHash, 0.25f);
            }
            animator.SetBool(isIdlingHash, isOn);
        }
    }
}
