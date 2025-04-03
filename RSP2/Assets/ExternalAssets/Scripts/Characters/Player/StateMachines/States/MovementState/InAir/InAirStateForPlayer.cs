using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class InAirStateForPlayer : MovementStateForPlayer
    {

        protected Vector3 horizontalMomentum;
        protected Vector3 verticalVelocityVector;
        protected bool isFirstFixedUpdate;


        public InAirStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }


        #region IStateMethods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorInAirParameter(true);

            horizontalMomentum = runtimeData.HorizontalMovementVector;

            verticalVelocityVector = Vector3.zero;
            isFirstFixedUpdate = true;
        }

        #endregion




    }
}
