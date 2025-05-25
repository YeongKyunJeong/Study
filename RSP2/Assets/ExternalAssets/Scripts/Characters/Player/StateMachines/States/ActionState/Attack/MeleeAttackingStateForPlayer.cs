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
    public class MeleeAttackingStateForPlayer : BaseAttackStateForPlayer
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

        public MeleeAttackingStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
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

            base.Exit();
            attackHitBox.Deactivate();
            attackHitBox.EnterEvent -= OnAttack;
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
                            gizmosDrawer.UpdateParameter(Vector3.zero, Vector3.zero, DetectionType.SphereCollider);
                            isDisabled = true;
                        }
                        else if (normalizedPassedTime >= hitBoxEnableTime)
                        {
                            CastRayAndSendResults();
                            isEnabled = true;
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
                            attackHitBox.Activate();
                            return;
                        }

                        break;
                    }
            }


        }

        private void CastRayAndSendResults()
        {
            if (!isEnabled)
            {
                combatSystem.ChangeStamina(-attackData.StaminaCost);
                combatSystem.ChangeMana(-attackData.MPCost);
                attackHitBox.StartRayCasting();
            }

            //attackSize = attackData.ColliderSize * currentWeapon.WeaponData.RangeModifier;
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
                VFXManager.PlayHitEffect(currentWeapon.WeaponData.DamageType, hitCombatSystem.MyUnit, hitPosition, attackVector.normalized);
                attackVector.y = 0;

                if (attackData.DamageType == DamageType.ByWeapon)
                {
                    hitCombatSystem.TakeDamage(statHandler.CurrentStatistics.Attack + attackData.Damage + currentWeapon.WeaponData.DamageBonus, currentWeapon.WeaponData.DamageType);

                }
                else
                {
                    hitCombatSystem.TakeDamage(statHandler.CurrentStatistics.Attack + attackData.Damage + currentWeapon.WeaponData.DamageBonus, attackData.DamageType);
                }
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


        protected virtual void SetAttackDetectorShape()
        {
            detectionType = attackData.DetectionType;
            attackSize = attackData.ColliderSize * currentWeapon.WeaponData.RangeModifier;
            switch (detectionType)
            {
                case DetectionType.SphereCollider:
                    {
                        useRaycast = false;
                        SphereCollider sphereCollider = attackHitBox.HitBoxCollider as SphereCollider;
                        sphereCollider.radius = attackSize.x * currentWeapon.WeaponData.RangeModifier;
                        sphereCollider.center = attackData.ColliderPosition;

                        player.RuntimeData.AttackPositionModifier = new Vector3(0, sphereCollider.center.y, 0);
                        break;
                    }

                case DetectionType.BoxCollider:
                    {
                        useRaycast = false;
                        BoxCollider BoxCollider = attackHitBox.HitBoxCollider as BoxCollider;
                        BoxCollider.size = attackSize;
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
        protected override void SetAnimatorPlayingSpeed(bool isExit = false)
        {
            base.SetAnimatorPlayingSpeed(isExit);

            if (isExit)
            {
                return;
            }
            animator.speed = player.CurrentWeapon.WeaponData.SpeedModifier * attackData.AttackSpeed * player.StatHandler.CurrentStatistics.AttackSpeed / 5;
        }


    }
}
