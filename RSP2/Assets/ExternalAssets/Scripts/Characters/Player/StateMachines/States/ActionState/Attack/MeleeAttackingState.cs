using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MeleeAttackingState : AttackStateForPlayer
    {
        //protected Collider hitBoxCollider;
        //protected Transform hitBoxTransform;

        protected AttackHitBox attackHitBox;
        protected float hitBoxEnableTime;
        protected float hitBoxDisableTime;

        //protected Vector3 targetDirVector;

        public MeleeAttackingState(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            attackHitBox = _player.AttackHitBox;

            //hitBoxCollider = _player.AttackHitBoxCollider;
        }

        public override void Enter()
        {
            base.Enter();

            mover.SetKeepRotate(true);
            //passedTime = 0;
            minimumDuration = attackStateData.BaseAttackData.AttackRecoveryTime;

            hitBoxEnableTime = attackStateData.BaseAttackData.HitBoxActivationTime;
            hitBoxDisableTime = Mathf.Min(attackStateData.BaseAttackData.HitBoxDeactivationTime, attackStateData.BaseAttackData.AttackRecoveryTime);
        }
        public override void CallUpdate()
        {
            base.CallUpdate();

            if (normalizedPassedTime >= hitBoxDisableTime)
            {
                attackHitBox.Deactivate();
            }
            else if (normalizedPassedTime >= hitBoxEnableTime)
            {
                attackHitBox.Activate();
            }
        }

        public override void Exit()
        {
            base.Exit();
            attackHitBox.Deactivate();
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
            horizontalMomentum = Vector3.Lerp(horizontalMomentum, Vector3.zero, 1 - Mathf.Exp(-5 * Time.deltaTime));
            return horizontalMomentum;
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

        protected virtual void SetHitBox()
        {
            SetHitBoxShape();
            SetHitBoxPosition();
        }

        protected virtual void SetHitBoxShape()
        {
            // To Do
        }

        protected virtual void SetHitBoxPosition()
        {
            // To Do
        }
    }
}
