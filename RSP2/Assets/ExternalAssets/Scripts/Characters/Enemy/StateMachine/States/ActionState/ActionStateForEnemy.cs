using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RSP2
{
    public class ActionStateForEnemy : IState
    {
        protected Enemy enemy;

        protected RuntimeDataForEnemy runtimeData;

        protected ActionStateMachineForEnemy stateMachine;
        protected StatHandlerForEnemy statHandler;
        protected MoverForEnemy mover;
        protected CharacterController controller;
        protected Animator animator;

        protected AnimatorStateInfo animationStateInfo;

        private Transform enemyTransform;

        private Collider[] hitColliders;
        private CombatSystem detectedCombatSystem;
        protected Vector3 moveDir;

        private Vector3 targetVector;
        public Vector3 TargetVector
        {
            get
            {
                if (!isTargetVectorThisFrame)
                {
                    GetAndSaveTargetVector();
                }
                return targetVector;
            }
        }
        private float targetDistanceSqr;
        public float TargetDistanceSqr
        {
            get
            {
                if (!isTargetVectorThisFrame)
                {
                    GetAndSaveTargetVector();
                }
                return targetDistanceSqr;
            }
        }

        private bool isTargetVectorThisFrame = false;


        public ActionStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine)
        {
            enemy = _enemy;
            stateMachine = _stateMachine;

            runtimeData = enemy.RuntimeData;

            statHandler = _enemy.StatHandler;
            mover = enemy.Mover;
            controller = enemy.Controller;
            animator = enemy.Animator;

            enemyTransform = enemy.transform;

        }

        #region IState Methods

        public virtual void Enter()
        {

        }

        public void Enter(int dataKey)
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void CallUpdate()
        {
            if (!runtimeData.IsHostile)
            {
                stateMachine.SetDefaultState();
                return;
            }

            isTargetVectorThisFrame = false;
        }

        public virtual void CallPhysicsUpdate()
        {
        }


        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitEvent()
        {
        }

        #endregion

        protected virtual void SetAnimatorSelfStateParameter(bool isOn) { }

        protected float GetNormalizedTime(Animator animator, string tag)
        {
            if (animator.IsInTransition(0))
            {
                animationStateInfo = animator.GetNextAnimatorStateInfo(0);
                return animationStateInfo.IsTag(tag) ? animationStateInfo.normalizedTime : -1f;
            }
            else
            {
                animationStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return animationStateInfo.IsTag(tag) ? animationStateInfo.normalizedTime : -1f;
            }
        }

        protected Vector3 GetAndSaveTargetVector()
        {
            targetVector = runtimeData.Target.transform.position - enemy.transform.position;
            targetDistanceSqr = targetVector.sqrMagnitude;
            isTargetVectorThisFrame = true;
            return targetVector;
        }

        protected bool SearchForTarget()
        {
            // To Do : Save result and return that if called more than once within one frame

            hitColliders = Physics.OverlapSphere(enemyTransform.position,
                runtimeData.SearchingDistance, enemy.SearchingLayerMask);

            foreach (Collider hit in hitColliders)
            {
                detectedCombatSystem = hit.GetComponent<CombatSystem>();

                if (detectedCombatSystem != null
                    && !detectedCombatSystem.IsDead
                    /*&& detectedCombatSystem.MyFaction != enemy.CombatSystem.MyFaction*/)
                {
                    switch (runtimeData.ChasingTargetType)
                    {
                        case ChasingTargetType.PlayerOnly:
                            {
                                if (detectedCombatSystem.MyFaction == Faction.Player)
                                {
                                    runtimeData.Target = detectedCombatSystem;

                                    //Debug.Log($"Target detected : {detectedCombatSystem.name}");
                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        case ChasingTargetType.AllFaction:
                            {
                                runtimeData.Target = detectedCombatSystem;

                                return true;

                            }
                        case ChasingTargetType.NotMyFaction:
                            {
                                if (detectedCombatSystem.MyFaction != enemy.CombatSystem.MyFaction)
                                {
                                    runtimeData.Target = detectedCombatSystem;

                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        default:
                            {
                                runtimeData.Target = null;
                                return false;
                            }
                    }
                }

            }

            runtimeData.Target = null;
            return false;
        }

        protected bool IsInAttackRange(bool useDistance = false)
        {
            if (runtimeData.Target == null) return false;

            if (runtimeData.Target.IsDead) return false;

            //if (stateMachine.CurrentAttackInfo == null)
            //    SelectAttack();

            if (useDistance)
            {
                //float playerDistanceSqr = (enemy.Target.transform.position - enemy.transform.position).sqrMagnitude;
                // TODO :: Compare with attack distance;
                if (TargetDistanceSqr <= runtimeData.AttackRangeSqr)
                {
                    return true;
                }

                return false;
                //return false;
            }


            //switch (stateMachine.CurrentAttackInfo.DetectionType)
            //{
            //    case DetectionType.WeaponCollider:
            //        return playerDistanceSqr <= 1.5f;

            //    case DetectionType.BoxCast:
            //        return playerDistanceSqr <= stateMachine.CurrentAttackInfo.BoxCastSize.z * stateMachine.CurrentAttackInfo.BoxCastSize.z;
            //}

            return false;
        }

        protected virtual bool IsInSight()
        {
            if (runtimeData.Target == null) return false;

            if (runtimeData.Target.IsDead) return false;

            //Vector3 directionToTarget = enemy.Target.transform.position - enemy.transform.position;
            Vector3 directionToTarget = TargetVector;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            Vector3 forward = enemy.transform.forward;
            forward.y = 0;
            forward.Normalize();

            float angleToTarget = Vector3.Angle(forward, directionToTarget);


            if (angleToTarget <= enemy.FieldOfView / 2f)
            {
                return true;
            }

            return false;
        }

    }
}
