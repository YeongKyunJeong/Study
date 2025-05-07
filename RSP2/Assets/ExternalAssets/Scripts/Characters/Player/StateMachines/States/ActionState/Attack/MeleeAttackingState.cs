using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace RSP2
{
    public class MeleeAttackingState : AttackStateForPlayer
    {
        //protected Collider hitBoxCollider;
        //protected Transform hitBoxTransform;

        protected AttackHitBox attackHitBox;
        protected float hitBoxEnableTime;
        protected float hitBoxDisableTime;

        protected Weapon currentWeapon;

        protected AttackData attackData;

        //protected Vector3 targetDirVector;

        public MeleeAttackingState(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            attackHitBox = _player.AttackHitBox;

            //hitBoxCollider = _player.AttackHitBoxCollider;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            currentWeapon = player.CurrentWeapon;

            animator.speed = player.CurrentWeapon.WeaponData.SpeedModifier * attackStateData.BaseAttackData.AttackSpeed;

            attackHitBox.EnterEvent += OnAttack;

            mover.SetKeepRotate(true);
            minimumDuration = attackStateData.BaseAttackData.AttackRecoveryTime;

            SetHitBoxShape();


            hitBoxEnableTime = attackStateData.BaseAttackData.HitBoxActivationTime;
            hitBoxDisableTime = Mathf.Min(attackStateData.BaseAttackData.HitBoxDeactivationTime, attackStateData.BaseAttackData.AttackRecoveryTime);
        }

        public override void Exit()
        {
            attackHitBox.EnterEvent -= OnAttack;

            base.Exit();
            attackHitBox.Deactivate();
            mover.SetKeepRotate(false);
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            if (normalizedPassedTime >= hitBoxDisableTime)
            {
                attackHitBox.Deactivate();
                return;
            }
            else if (normalizedPassedTime >= hitBoxEnableTime)
            {
                attackHitBox.Activate();
                return;
            }
        }

        #endregion


        //public override void CallUpdate()
        //{
        //    //passedTime += Time.deltaTime;

        //    // To Do: Falling while attack state;
        //    //mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
        //    //horizontalMomentum = Vector3.Lerp(horizontalMomentum, Vector3.zero, 1 - Mathf.Exp(-3 * Time.deltaTime));

        //    //base.CallUpdate();

        //    //Debug.Log(horizontalMomentum);

        //}

        protected virtual void OnAttack(CombatSystem hitCombatSystem, Collider hitCollider)
        {
            if (combatSystem.MyFaction != hitCombatSystem.MyFaction)
            {
                hitCombatSystem.TakeDamage(-attackData.Damage - currentWeapon.WeaponData.DamageBonus, currentWeapon.WeaponData.AttackDamageType);
                Vector3 attackPosition = player.transform.position + player.RuntimeData.AttackPositionModifier;
                Vector3 hitPosition = hitCollider.ClosestPoint(attackPosition);
                VFXManager.PlayHitEffect(currentWeapon.WeaponData.AttackDamageType, hitCombatSystem.MyUnit, hitPosition, (attackPosition - hitPosition).normalized);
            }
        }

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

        protected override void SetAnimatorPlayingSpeed(bool isExit = false)
        {
            base.SetAnimatorPlayingSpeed(isExit);

            if (isExit)
            {
                return;
            }
            animator.speed = player.CurrentWeapon.WeaponData.SpeedModifier * attackData.AttackSpeed;


        }

        protected virtual void SetHitBoxShape()
        {
            // TODO:: Add other shape collider case
            switch (attackData.DetectionType)
            {
                case DetectionType.SphereCollider:
                    {
                        SphereCollider sphereCollider = attackHitBox.HitBoxCollider as SphereCollider;
                        sphereCollider.radius = attackData.ColliderSize.x * currentWeapon.WeaponData.RangeModifier;
                        sphereCollider.center = attackData.ColliderPosition;

                        player.RuntimeData.AttackPositionModifier = new Vector3(0, sphereCollider.center.y, 0);
                        break;
                    }

                case DetectionType.BoxCollider:
                    {
                        BoxCollider BoxCollider = attackHitBox.HitBoxCollider as BoxCollider;
                        BoxCollider.size = attackData.ColliderSize;
                        BoxCollider.center = attackData.ColliderPosition;

                        player.RuntimeData.AttackPositionModifier = new Vector3(0, BoxCollider.center.y, 0);
                        break;
                    }

                default:
                    break;
            }
        }


    }
}
