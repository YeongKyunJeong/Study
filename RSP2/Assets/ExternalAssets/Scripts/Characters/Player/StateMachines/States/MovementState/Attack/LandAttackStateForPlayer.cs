using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class LandAttackStateForPlayer : AttackStateForPlayer
    {

        private float temporaryAttackDuration = 1;

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


    }
}
