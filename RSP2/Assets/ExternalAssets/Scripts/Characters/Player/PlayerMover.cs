using UnityEngine;

namespace RSP2
{
    public class PlayerMover : MonoBehaviour
    {
        private Player player;
        private PlayerScriptableObject sOData;
        private MovementStateDataForPlayer movementStateData;
        private PlayerRuntimeData runtimeData;
        private PlayerInputReader inputReader;
        private CharacterController controller;

        private Vector2 movementInputVector;
        private Vector3 nextHorizontalMovementVector;
        private Transform mainCameraTransform;
        private Vector3 nextVerticalVelocityVector;

        //private bool isFirstJumpForceUpdate;

        //private Vector3 gravity;

        public void Initialize()
        {
            player = GetComponent<Player>();
            sOData = player.SOData;
            movementStateData = sOData.MovementStateData;
            runtimeData = player.RuntimeData;
            inputReader = GetComponent<PlayerInputReader>();
            controller = player.Controller;
            mainCameraTransform = Camera.main.transform;

            //inputReader.MoveEvent += OnMoveInput;
            //inputReader.JumpEvent += OnJumpInput;

            //gravity = Physics.gravity;

            //isFirstJumpForceUpdate = false;
            //isInAir = false;
            nextVerticalVelocityVector = Vector3.zero;
        }

        public void CallFixedUpdate()
        {
            //UpdateNextVerticalMovement();

            //UpdateHorizontalMovementInputResult();

            ApplyUpdatedMovement();

            return;
        }

        //private void OnJumpInput()
        //{
        //    if (isInAir)
        //    {
        //        return;
        //    }

        //    isInAir = true;
        //    isFirstJumpForceUpdate = true;
        //}

        //private void DoJump()
        //{

        //}

        private void ApplyUpdatedMovement()
        {
            controller.Move((nextHorizontalMovementVector + nextVerticalVelocityVector) * Time.fixedDeltaTime);
            if (nextHorizontalMovementVector == Vector3.zero)
            {
                //Debug.Log("No Input : Mover");
                return;
            }
            Rotate(nextHorizontalMovementVector);
        }

        public void UpdateNextVerticalVelocityVector(Vector3 velocityVector)
        {
            //if (isInAir)
            //{
            //    if (isFirstJumpForceUpdate)
            //    {
            //        verticalVelocityVector.y = movementStateData.JumpForceModifier;
            //        isFirstJumpForceUpdate = false;
            //        return;
            //    }

            //    if (controller.isGrounded)
            //    {
            //        isInAir = false;
            //        Debug.Log("isGrounded");
            //    }

            //    verticalVelocityVector += gravity * Time.fixedDeltaTime;
            //}
            nextVerticalVelocityVector = velocityVector;
        }

        public void UpdateNextHorizontalMovementVector(Vector3 movementVector)
        {
            nextHorizontalMovementVector = movementVector;
        }

        private void Rotate(Vector3 targetDir)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
                Time.fixedDeltaTime * movementStateData.RotationSpeedModifier);
        }


    }
}
