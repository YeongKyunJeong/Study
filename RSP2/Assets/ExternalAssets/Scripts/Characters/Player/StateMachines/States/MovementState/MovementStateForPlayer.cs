using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MovementStateForPlayer : IState
    {
        protected Player player;

        protected PlayerRuntimeData runtimeData;
        protected MovementStateDataForPlayer movementStateData;
        protected AttackStateDataForPlayer attackStateData;

        protected Transform mainCameraTransform;

        protected MovementStateMachineForPlayer stateMachine;
        protected PlayerInputReader inputReader;
        protected PlayerMover mover;
        protected CharacterController controller;
        protected Animator animator;

        protected AnimatorStateInfo animationStateInfo;
        protected readonly int onLandHash = Animator.StringToHash("@OnLand");
        protected readonly int inAirHash = Animator.StringToHash("@InAir");
        protected readonly int attackHash = Animator.StringToHash("@Attack");

        private RaycastHit hit;
        private Vector3 slopeDetectingRayVector;
        private float slopeDetectingRayMaxDistance;
        private Vector3 slopeDetectingRayStartHeightVector;
        private Vector3 floatingHeightVector;
        private LayerMask groundLayer;
        private Transform playerTransform;

        protected Vector2 moveInput;
        //protected float fixedDeltaTime;

        public MovementStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine)
        {
            player = _player;

            runtimeData = player.RuntimeData;
            movementStateData = player.SOData.MovementStateData;
            attackStateData = player.SOData.AttackStateData;

            //mainCameraTransform = Camera.main.transform;

            stateMachine = _stateMachine;
            inputReader = player.InputReader;
            mover = player.Mover;
            controller = player.Controller;
            animator = player.Animator;
            //fixedDeltaTime = Time.fixedDeltaTime;


            playerTransform = player.transform;

            slopeDetectingRayStartHeightVector = Vector3.up * movementStateData.SlopeDetectingRayStartHeight;
            slopeDetectingRayVector = Vector3.down * (movementStateData.RaycastDistance + movementStateData.SlopeDetectingRayStartHeight);
            slopeDetectingRayMaxDistance = movementStateData.RaycastDistance + movementStateData.SlopeDetectingRayStartHeight;
            floatingHeightVector = Vector3.up * (movementStateData.FloatingHeight);

            groundLayer = movementStateData.GroundLayer;

        }

        #region IState Methods

        public virtual void Enter()
        {
            inputReader.MoveEvent += OnMoveInput;
            inputReader.JumpEvent += OnJumpInput;
            inputReader.WalkToggleEvent += OnWalkToggleInput;
            inputReader.DashEvent += OnDashInput;
            inputReader.AttackEvent += OnAttackInput;

            moveInput = runtimeData.MoveInput;
        }

        public virtual void Exit()
        {
            inputReader.MoveEvent -= OnMoveInput;
            inputReader.JumpEvent -= OnJumpInput;
            inputReader.WalkToggleEvent -= OnWalkToggleInput;
            inputReader.DashEvent -= OnDashInput;
            inputReader.AttackEvent -= OnAttackInput;
        }

        public virtual void CallUpdate()
        {
            //Move();

        }


        public virtual void CallPhysicsUpdate()
        {
        }

        //public virtual void HandleInput()
        //{
        //}

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


        #region Movement Input Method

        protected virtual void OnMoveInput(Vector2 _moveInput)
        {
            moveInput = _moveInput;
            runtimeData.MoveInput = moveInput;
        }

        protected virtual void OnJumpInput() { }

        protected virtual void OnWalkToggleInput()
        {
            runtimeData.IsWalking = !runtimeData.IsWalking;
        }

        protected virtual void OnDashInput() { }

        protected virtual void OnAttackInput() { }

        #endregion


        protected virtual void SetAnimatorSelfStateParameter(bool isOn) { }

        protected virtual void SetAnimatorOnLandParameter(bool isOn)
        {
            animator.SetBool(onLandHash, isOn);
        }

        protected virtual void SetAnimatorInAirParameter(bool isOn)
        {
            animator.SetBool(inAirHash, isOn);
        }

        protected virtual void SetAnimatorIsAttackingParameter(bool isOn)
        {
            animator.SetBool(attackHash, isOn);
        }


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

        protected virtual Vector3 CheckIsSlope(bool stickFloor = true)
        {
            //Debug.DrawRay(playerTransform.position + slopeDetectingRayStartHeightVector, slopeDetectingRayVector, Color.green);

            if (Physics.Raycast(playerTransform.position + slopeDetectingRayStartHeightVector, Vector3.down, out hit, slopeDetectingRayMaxDistance,
                groundLayer))
            {
                return hit.normal;

            }

            return Vector3.down;

        }

    }

    public static class FallingCalculator
    {
        private static Vector3 gravity = Physics2D.gravity;
        private static float fallingThreshold;

        public static void ApplyFallingToVector(ref Vector3 velocityVector, float timeDelta)
        {
            velocityVector += timeDelta * gravity;
            return;
        }

        public static bool CheckFalling(Vector3 fallingVelocityVector, Vector3 slopeNormalVector, CharacterController controller)
        {
            if (slopeNormalVector.y < -0.98f)
            {
                fallingThreshold = 5 * Physics.gravity.y * Time.deltaTime;
                if (!controller.isGrounded && (fallingVelocityVector.y < fallingThreshold))
                {
                    return true;
                }

            }

            return false;
        }

    }



    public static class InputToDirectionVectorConverter
    {
        static Transform mainCameraTransform = Camera.main.transform;
        //static Vector3 horizontalMovementVector;
        private static Vector3 forward;
        private static Vector3 right;

        private static Vector3 vectorOnXZ;
        private static Vector3 vectorOnSlope;

        public static Vector3 ConvertInputToMovementDirectionVector(Vector3 input)
        {
            forward = mainCameraTransform.forward;
            right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return (forward * input.y + right * input.x);
        }

        public static Vector3 ConvertInputToMovementDirectionVectorOnSlope(Vector3 input, Vector3 normal)//////////////////////// To Do:
        {
            vectorOnXZ = ConvertInputToMovementDirectionVector(input);

            vectorOnSlope = (vectorOnXZ - Vector3.Dot(vectorOnXZ, normal) * normal).normalized;

            return vectorOnSlope;
        }

    }



}
