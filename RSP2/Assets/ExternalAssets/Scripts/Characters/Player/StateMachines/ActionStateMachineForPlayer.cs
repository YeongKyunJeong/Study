using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ActionStateMachineForPlayer : StateMachine
    {
        private Player player;
        private MoverForPlayer mover;
        private Animator animator;

        private readonly int instantHitHash = Animator.StringToHash("Hit");
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

        public SkillMeleeAttackingStateForPlayer[] SkillMeleeAttackingStates { get; private set; }
        public RangeAttackingStateForPlayer[] RangeAttackingStates { get; private set; }
        #endregion

        #endregion


        public ActionStateMachineForPlayer(Player _player)
        {

            player = _player;

            mover = player.Mover;

            animator = player.Animator;


            IdlingState = new IdlingStateForPlayer(_player, this);

            WalkingState = new WalkingStateForPlayer(_player, this);

            RunnigState = new RunnigStateForPlayer(_player, this);

            LandDashingState = new LandDashingStateForPlayer(_player, this);


            JumpingState = new JumpingStateForPlayer(_player, this);

            FallingState = new FallingStateForPlayer(_player, this);


            BasicMeleeAttackingState = new BasicMeleeAttackingStateForPlayer(_player, this);

            SkillMeleeAttackingStates = new SkillMeleeAttackingStateForPlayer[2];
            RangeAttackingStates = new RangeAttackingStateForPlayer[2];

            for (int i = 0; i < SkillMeleeAttackingStates.Length; i++)
            {
                SkillMeleeAttackingStates[i] = new SkillMeleeAttackingStateForPlayer(_player, this);
            }
            for (int i = 0; i < SkillMeleeAttackingStates.Length; i++)
            {
                RangeAttackingStates[i] = new RangeAttackingStateForPlayer(_player, this);
            }

            SetDefaultState();
        }

        public override void SetDefaultState()
        {
            ChangeState(IdlingState);
        }

        public void OnHit()
        {
            // TODO :: Add force
            animator.CrossFadeInFixedTime(instantHitHash, 0.25f);
        }

        public virtual bool CheckAttackResources(AttackData attackData, CombatSystem combatSystem)
        {
            if (combatSystem.CurrentMP >= attackData.MPCost)
            {
                if (combatSystem.CurrentStamina >= attackData.StaminaCost)
                {
                    return true;
                }
            }

            return false;
        }


        public void OnDie()
        {
            mover.UpdateNextHorizontalMovementVector(Vector3.zero);
            mover.UpdateNextVerticalVelocityVector(Vector3.zero);
            animator.CrossFadeInFixedTime(instantDyingHash, 0.25f);
            currentState = null; // TODO :: Add dyingState
        }
    }


}
