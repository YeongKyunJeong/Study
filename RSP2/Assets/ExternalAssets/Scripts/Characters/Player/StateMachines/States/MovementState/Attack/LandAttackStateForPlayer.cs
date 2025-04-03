using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class LandAttackStateForPlayer : AttackStateForPlayer
    {

        private float temporaryAttackDuration = 1;
        //protected readonly int landAttackingHash = Animator.StringToHash("IsLandAttacking");

        // To Do : Add combo attack


        public LandAttackStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }


        #region IState Methods



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

            //animator.SetBool(landAttackingHash, isOn);
        }


    }
}
