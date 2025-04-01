using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class LandAttackStateForPlayer : AttackStateForPlayer
    {
        protected readonly int landAttackHash = Animator.StringToHash("isAttacking");

        private float temporaryAttackDuration = 1;

        // To Do : Add combo attack


        public LandAttackStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);

            passedTime = 0;
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }


        public override void CallUpdate()
        {
            base.CallUpdate();

            passedTime += Time.deltaTime;

            // Temporary state exit logic

            if (passedTime > 1)
            {
                SetAnimatorOnLandParameter(true);
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

        }


        #endregion

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

            animator.SetBool(landAttackHash, isOn);
        }
    }
}
