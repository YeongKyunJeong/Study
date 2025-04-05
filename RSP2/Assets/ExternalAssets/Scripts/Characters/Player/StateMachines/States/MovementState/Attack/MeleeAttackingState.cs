using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MeleeAttackingState : AttackStateForPlayer
    {
        protected Collider attackHitBox;
        protected float hitBoxEnableTime;
        protected float hitBoxDisableTime;
        //protected Vector3 targetDirVector;

        public MeleeAttackingState(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            attackHitBox = _player.AttackHitBox;
        }

        public override void Enter()
        {
            base.Enter();

            mover.SetKeepRotate(true);
            //passedTime = 0;
            minimumDuration = attackStateData.AttackRecoveryMultiplier;

            hitBoxEnableTime = attackStateData.AttackHitBoxEnableMultiplier;
            hitBoxDisableTime = Mathf.Min(attackStateData.AttackHitBoxDiableMultiplier, attackStateData.AttackRecoveryMultiplier);
        }
        public override void CallUpdate()
        {
            base.CallUpdate();

            if (normalizedPassedTime >= hitBoxDisableTime)
            {
                attackHitBox.enabled = false;
            }
            else if (normalizedPassedTime >= hitBoxEnableTime)
            {
                attackHitBox.enabled = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
            if (attackHitBox.enabled)
                attackHitBox.enabled = false;
            mover.SetKeepRotate(false);
        }


        //public override void CallUpdate()
        //{
        //    //passedTime += Time.deltaTime;

        //    // To Do: Falling while attack state;
        //    //mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
        //    //horizontalMomentum = Vector3.Lerp(horizontalMomentum, Vector3.zero, 1 - Mathf.Exp(-3 * Time.deltaTime));

        //    //base.CallUpdate();

        //    //Debug.Log(horizontalMomentum);

        //}

        protected override Vector3 CalculateThisUpdateMomentum()
        {
            horizontalMomentum = Vector3.Lerp(horizontalMomentum, Vector3.zero, 1 - Mathf.Exp(-3 * Time.deltaTime));
            return base.CalculateThisUpdateMomentum();
        }

        protected override bool CheckIsCancelable()
        {
            //return base.CheckIsCancelable();
            if (normalizedPassedTime > minimumDuration)
            {
                return true;
            }

            return false;
        }
    }
}
