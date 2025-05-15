using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RSP2
{
    public enum ChasingTargetTpye
    {
        PlayerOnly,
        AllFaction,
        NotMyFaction
    }


    public class ActionStateForEnemy : IState
    {
        protected Enemy enemy;

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

        protected readonly int inAirHash = Animator.StringToHash("@InAir");


        public ActionStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine)
        {
            enemy = _enemy;
            stateMachine = _stateMachine;

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
            targetVector = enemy.Target.transform.position - enemy.transform.position;
            targetDistanceSqr = targetVector.sqrMagnitude;
            isTargetVectorThisFrame = true;
            return targetVector;
        }

        protected bool SearchForTaget()
        {
            // To Do : Save result and return that if called more than once within one frame

            hitColliders = Physics.OverlapSphere(enemyTransform.position,
                enemy.SearchingDistance, enemy.SearchingLayerMask);

            foreach (Collider hit in hitColliders)
            {
                detectedCombatSystem = hit.GetComponent<CombatSystem>();

                if (detectedCombatSystem != null
                    && !detectedCombatSystem.IsDead
                    /*&& detectedCombatSystem.MyFaction != enemy.CombatSystem.MyFaction*/)
                {
                    switch (enemy.ChasingTargetType)
                    {
                        case ChasingTargetTpye.PlayerOnly:
                            {
                                if (detectedCombatSystem.MyFaction == Faction.Player)
                                {
                                    enemy.Target = detectedCombatSystem;

                                    Debug.Log($"Target detected : {detectedCombatSystem.name}");
                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        case ChasingTargetTpye.AllFaction:
                            {
                                enemy.Target = detectedCombatSystem;

                                Debug.Log($"Target detected : {detectedCombatSystem.name}");
                                return true;

                            }
                        case ChasingTargetTpye.NotMyFaction:
                            {
                                if (detectedCombatSystem.MyFaction != enemy.CombatSystem.MyFaction)
                                {
                                    enemy.Target = detectedCombatSystem;

                                    Debug.Log($"Target detected : {detectedCombatSystem.name}");
                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        default:
                            {
                                enemy.Target = null;
                                return false;
                            }
                    }
                }

            }

            enemy.Target = null;
            return false;
        }

        protected bool IsInAttackRange(bool useDistance = false)
        {
            if (enemy.Target == null) return false;

            if (enemy.Target.IsDead) return false;

            //if (stateMachine.CurrentAttackInfo == null)
            //    SelectAttack();

            if (useDistance)
            {
                //float playerDistanceSqr = (enemy.Target.transform.position - enemy.transform.position).sqrMagnitude;
                // TODO :: Compare with attack distance;
                if (TargetDistanceSqr <= enemy.AttackRangeSqr)
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
            if (enemy.Target == null) return false;

            if (enemy.Target.IsDead) return false;

            //Vector3 directionToTarget = enemy.Target.transform.position - enemy.transform.position;
            Vector3 directionToTarget = TargetVector;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            Vector3 forward = enemy.transform.forward;
            forward.y = 0;
            forward.Normalize();

            float angleToTaget = Vector3.Angle(forward, directionToTarget);


            if (angleToTaget <= enemy.FieldOfView / 2f)
            {
                return true;
            }

            return false;
        }

    }
}
