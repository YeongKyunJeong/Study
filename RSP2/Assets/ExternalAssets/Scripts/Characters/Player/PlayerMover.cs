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

        private float fixedDeltaTime;

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

            nextVerticalVelocityVector = Vector3.zero;

            fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void CallFixedUpdate()
        {
            ApplyUpdatedMovement();
            return;
        }

        private void ApplyUpdatedMovement()
        {
            controller.Move((nextHorizontalMovementVector + nextVerticalVelocityVector) * fixedDeltaTime);
            //Debug.Log((nextHorizontalMovementVector + nextVerticalVelocityVector).y);
            if (nextHorizontalMovementVector == Vector3.zero)
            {
                //Debug.Log("No Input : Mover");
                return;
            }
            Rotate(nextHorizontalMovementVector);
            nextVerticalVelocityVector = 0.1f*Vector3.down; ///////// To Do: Fix bouncing problem when going to down hill
        }

        public void UpdateNextVerticalVelocityVector(Vector3 velocityVector)
        {
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
