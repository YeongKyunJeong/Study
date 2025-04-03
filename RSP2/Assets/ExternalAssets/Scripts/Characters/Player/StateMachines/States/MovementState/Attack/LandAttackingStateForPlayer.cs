using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class LandAttackingStateForPlayer : AttackStateForPlayer
    {
        //protected readonly int landAttackingHash = Animator.StringToHash("IsLandAttacking");

        // To Do : Add combo attack
        

        public LandAttackingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            attackMinimumDuration = attackStateData.AttackRecoveryTime * attackStateData.AttackSpeedMultiplier;

        }



        public override void CallUpdate()
        {
            base.CallUpdate();

            passedTime += Time.deltaTime;

            // To Do: Falling while attack state;
            mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
            horizontalMomentum = Vector3.Lerp(horizontalMomentum, Vector3.zero, 1 - Mathf.Exp(-3 * Time.deltaTime));
            //Debug.Log(horizontalMomentum);

            if (passedTime > attackMinimumDuration)
            {

                SetAnimatorIsAttackingParameter(false);

                if (CheckIsSlope().y < -0.98) // No collider detected
                {
                    stateMachine.ChangeState(stateMachine.FallingState);
                    return;
                }

                if (moveInput == Vector2.zero)
                {
                    stateMachine.ChangeState(stateMachine.IdlingState);
                    return;
                }

                if (runtimeData.IsWalking)
                {
                    stateMachine.ChangeState(stateMachine.WalkingState);
                    return;
                }
                stateMachine.ChangeState(stateMachine.RunnigState);
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
