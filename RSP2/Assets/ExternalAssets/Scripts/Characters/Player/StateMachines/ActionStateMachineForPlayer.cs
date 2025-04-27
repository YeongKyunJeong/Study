using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ActionStateMachineForPlayer : StateMachine
    {
        private Player player;

        private readonly int instantDyingHash = Animator.StringToHash("Dying");

        #region Action States

        #region OnLand States

        public IdlingStateForPlayer IdlingState { get; private set; }
        public WalkingStateForPlayer WalkingState { get; private set; }
        public RunnigStateForPlayer RunnigState { get; private set; }
        public LandDashingStateForPlayer LandDashingState { get; private set; }

        #endregion


        #region InAir States

        public JumpingStateForPlayer JumpingState { get; private set; }
        public FallingStateForPlayer FallingState { get; private set; }

        #endregion


        #region Attack States

        public BasicMeleeAttackingStateForPlayer BasicMeleeAttackingState { get; private set; }

        #endregion

        #endregion


        public ActionStateMachineForPlayer(Player _player)
        {

            player = _player;

            IdlingState = new IdlingStateForPlayer(_player, this);

            WalkingState = new WalkingStateForPlayer(_player, this);

            RunnigState = new RunnigStateForPlayer(_player, this);

            LandDashingState = new LandDashingStateForPlayer(_player, this);


            JumpingState = new JumpingStateForPlayer(_player, this);

            FallingState = new FallingStateForPlayer(_player, this);


            BasicMeleeAttackingState = new BasicMeleeAttackingStateForPlayer(_player, this);


            SetDefaultState();
        }

        public override void SetDefaultState()
        {
            ChangeState(IdlingState);
        }

        public void OnDie()
        {
            player.Animator.CrossFadeInFixedTime(instantDyingHash, 0.25f);
            currentState = null; // TODO :: Add dyingState
        }
    }


}
