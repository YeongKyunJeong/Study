using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

namespace RSP2
{
    public class MeleeAttackingState : AttackStateForPlayer
    {
        //protected Collider hitBoxCollider;
        //protected Transform hitBoxTransform;

        protected AttackHitBox attackHitBox;
        protected float vFXStartTime;
        protected float hitBoxEnableTime;
        protected float hitBoxDisableTime;

        protected Ray ray;
        RaycastHit[] hits;
        protected bool useRaycast;
        protected DetectionType detectionType;
        protected GizmosDrawer gizmosDrawer;
        protected Vector3 attackSize;

        protected bool vFXStarted;
        protected bool isEnabled;
        protected bool isDisabled;
        protected Weapon currentWeapon;



        //protected Vector3 targetDirVector;

        public MeleeAttackingState(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            attackHitBox = _player.AttackHitBox;
            gizmosDrawer = player.GetComponent<GizmosDrawer>();
            //hitBoxCollider = _player.AttackHitBoxCollider;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            currentWeapon = player.CurrentWeapon;
            attackHitBox.EnterEvent += OnAttack;
            mover.SetKeepRotate(true);
            SetAttackDetectorShape();
            isDisabled = false;
            isEnabled = false;

            //animator.speed = player.CurrentWeapon.WeaponData.SpeedModifier * attackDataLibrary.BaseAttackData.AttackSpeed;
            //minimumDuration = attackDataLibrary.BaseAttackData.AttackRecoveryTime;
            //hitBoxEnableTime = attackDataLibrary.BaseAttackData.HitBoxActivationTime;
            //hitBoxDisableTime = Mathf.Min(attackDataLibrary.BaseAttackData.HitBoxDeactivationTime, attackDataLibrary.BaseAttackData.AttackRecoveryTime);
            animator.speed = player.CurrentWeapon.WeaponData.SpeedModifier * attackData.AttackSpeed;
            minimumDuration = attackData.AttackRecoveryTime;
            if (attackData.VFXName.Length > 0)
            {
                vFXStartTime = attackData.VFXStartTime;
                vFXStarted = false;
            }
            else vFXStarted = true;
            hitBoxEnableTime = attackData.HitBoxActivationTime;
            hitBoxDisableTime = Mathf.Min(attackData.HitBoxDeactivationTime, attackData.AttackRecoveryTime);
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

            if (isDisabled) { return; }

            if (!vFXStarted)
            {
                if (normalizedPassedTime >= vFXStartTime)
                {
                    VFXManager.PlayVFXEffect(attackData.VFXName, player.transform.position + player.RuntimeData.AttackPositionModifier, player.transform.forward);
                    vFXStarted = true;
                }
            }

            switch (useRaycast)
            {
                case true:
                    {
                        if (normalizedPassedTime >= hitBoxDisableTime)
                        {
                            isDisabled = true;
                            gizmosDrawer.UpdateParameter(Vector3.zero, Vector3.zero, DetectionType.SphereCollider);
                        }
                        else if (normalizedPassedTime >= hitBoxEnableTime)
                        {
                            isEnabled = true;
                            CastRayAndSendResults();
                            return;
                        }
                        break;
                    }
                default:
                    {
                        if (normalizedPassedTime >= hitBoxDisableTime)
                        {
                            isDisabled = true;
                            attackHitBox.Deactivate();
                            return;
                        }

                        if (isEnabled) return;

                        if (normalizedPassedTime >= hitBoxEnableTime)
                        {
                            combatSystem.ChangeStamina(-attackData.StaminaCost);
                            combatSystem.ChangeMana(-attackData.MPCost);
                            isEnabled = true;
                            return;
                        }

                        break;
                    }
            }


        }

        private void CastRayAndSendResults()
        {
            attackSize = attackData.ColliderSize * currentWeapon.WeaponData.RangeModifier;
            switch (detectionType)
            {
                case DetectionType.SphereRaycast:
                    {
                        hits = Physics.SphereCastAll(player.transform.position + player.transform.TransformDirection(attackData.ColliderPosition), attackSize.x, player.transform.position, 1f, attackHitBox.TargetLayerMask);
                    }
                    break;
                case DetectionType.BoxRaycast:
                    {
                        hits = Physics.BoxCastAll(player.transform.position + player.transform.TransformDirection(attackData.ColliderPosition), attackSize, player.transform.forward, player.transform.rotation, 1f, attackHitBox.TargetLayerMask);
                    }
                    break;
                default:
                    break;
            }

            attackHitBox.SendRaycastHitsResults(hits);
            gizmosDrawer.UpdateParameter(player.transform.position + player.transform.TransformDirection(attackData.ColliderPosition), attackSize, detectionType);
        }

        #endregion


        protected virtual void OnAttack(CombatSystem hitCombatSystem, Collider hitCollider)
        {
            if (combatSystem.MyFaction != hitCombatSystem.MyFaction)
            {
                Vector3 attackPosition = player.transform.position + player.RuntimeData.AttackPositionModifier;
                Vector3 hitPosition = hitCollider.ClosestPoint(attackPosition);
                Vector3 attackVector = attackPosition - hitPosition;
                VFXManager.PlayHitEffect(currentWeapon.WeaponData.AttackDamageType, hitCombatSystem.MyUnit, hitPosition, attackVector.normalized);
                attackVector.y = 0;
                hitCombatSystem.TakeDamage(attackData.Damage + currentWeapon.WeaponData.DamageBonus, currentWeapon.WeaponData.AttackDamageType);
                hitCombatSystem.TakeForce(-attackVector.normalized * attackData.PushForce);
            }
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

        protected virtual void SetAttackDetectorShape()
        {
            // TODO:: Add other shape collider case
            detectionType = attackData.DetectionType;
            switch (detectionType)
            {
                case DetectionType.SphereCollider:
                    {
                        useRaycast = false;
                        SphereCollider sphereCollider = attackHitBox.HitBoxCollider as SphereCollider;
                        sphereCollider.radius = attackData.ColliderSize.x * currentWeapon.WeaponData.RangeModifier;
                        sphereCollider.center = attackData.ColliderPosition;

                        player.RuntimeData.AttackPositionModifier = new Vector3(0, sphereCollider.center.y, 0);
                        break;
                    }

                case DetectionType.BoxCollider:
                    {
                        useRaycast = false;
                        BoxCollider BoxCollider = attackHitBox.HitBoxCollider as BoxCollider;
                        BoxCollider.size = attackData.ColliderSize;
                        BoxCollider.center = attackData.ColliderPosition;

                        player.RuntimeData.AttackPositionModifier = new Vector3(0, BoxCollider.center.y, 0);
                        break;
                    }
                case DetectionType.SphereRaycast:
                    {
                        useRaycast = true;
                        player.RuntimeData.AttackPositionModifier = new Vector3(0, attackData.ColliderPosition.y, 0);
                        break;
                    }

                default:
                    {
                        useRaycast = true;
                        player.RuntimeData.AttackPositionModifier = new Vector3(0, attackData.ColliderPosition.y, 0);
                        break;
                    }
            }
        }


    }
}
