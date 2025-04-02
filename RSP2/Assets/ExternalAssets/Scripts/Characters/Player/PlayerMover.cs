using UnityEngine;

namespace RSP2
{
    public class PlayerMover : MonoBehaviour
    {
        private Player player;
        private PlayerScriptableObject sOData;
        private MovementStateDataForPlayer movementStateData;
        private AttackStateDataForPlayer attackStateData;
        private PlayerRuntimeData runtimeData;
        private PlayerInputReader inputReader;
        private CharacterController controller;

        private Vector2 movementInputVector;
        private Vector3 nextHorizontalMovementVector;
        private Transform mainCameraTransform;
        private Vector3 nextVerticalVelocityVector;

        private bool needToSetHeight = false;
        private Vector3 targetHeight;

        //private float fixedDeltaTime;

        //private bool isFirstJumpForceUpdate;

        //private Vector3 gravity;

        public void Initialize()
        {
            player = GetComponent<Player>();
            sOData = player.SOData;
            movementStateData = sOData.MovementStateData;
            attackStateData = sOData.AttackStateData;
            runtimeData = player.RuntimeData;
            inputReader = GetComponent<PlayerInputReader>();
            controller = player.Controller;
            mainCameraTransform = Camera.main.transform;

            nextVerticalVelocityVector = Vector3.zero;

            //fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void CallFixedUpdate()
        {
            return;
        }

        public void CallUpdate()
        {
            ApplyUpdatedMovement();
            return;
        }

        private void ApplyUpdatedMovement()
        {

            controller.Move((nextHorizontalMovementVector + nextVerticalVelocityVector) * Time.deltaTime);
            //if (needToSetHeight)
            //{
            //    needToSetHeight = false;
            //    this.enabled = false;
            //    transform.position = targetHeight;
            //    this.enabled = true;

            //}

            //Debug.Log((nextHorizontalMovementVector + nextVerticalVelocityVector).y);
            if (nextHorizontalMovementVector == Vector3.zero)
            {
                return;
            }

            nextHorizontalMovementVector.y = 0;
            Rotate(nextHorizontalMovementVector);

            nextVerticalVelocityVector = 5*Time.deltaTime * Physics.gravity;

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
                Time.deltaTime * movementStateData.RotationSpeedModifier);
        }

        public void SetHeightManually(Vector3 targetHeightVector)
        {
            needToSetHeight = true;
            targetHeight = targetHeightVector;
        }
    }
}
