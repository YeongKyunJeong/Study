using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class OnLandStateForEnemy : ActionStateForEnemy
    {

        protected readonly int onLandHash = Animator.StringToHash("@OnLand");

        public OnLandStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
        }


        #region IStateMethods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorOnLandParameter(true);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        #endregion

        protected virtual void SetAnimatorOnLandParameter(bool isOn)
        {
            animator.SetBool(onLandHash, isOn);
        }

    }
}
