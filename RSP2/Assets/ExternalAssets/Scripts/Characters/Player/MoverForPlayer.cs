using UnityEngine;

namespace RSP2
{
    public class MoverForPlayer : MonoBehaviour
    {
        private Player player;
        private PlayerScriptableObject sOData;
        private MovementStateDataForPlayer movementStateData;
        private AttackDataLibrary attackData;
        private RuntimeDataForPlayer runtimeData;
        private PlayerInputReader inputReader;
        private CharacterController controller;

        private Vector2 movementInputVector;
        private Vector3 nextHorizontalMovementVector;
        private Vector3 nextForceVector;
        private Vector3 nextRotationVector;
        private Transform mainCameraTransform;
        private Vector3 nextVerticalVelocityVector;
        private bool keepRotation = false;

        private bool needToSetHeight = false;
        private Vector3 targetHeight;

        //private float fixedDeltaTime;

        //private bool isFirstJumpForceUpdate;

        //private Vector3 gravity;

        public void Initialize(Player _player)
        {
            player = _player;
            sOData = player.SOData;
            movementStateData = sOData.MovementStateData;
            attackData = sOData.AttackDataLibrary;
            runtimeData = player.RuntimeData;
            inputReader = GetComponent<PlayerInputReader>();
            controller = player.Controller;
            mainCameraTransform = Camera.main.transform;

            nextVerticalVelocityVector = Vector3.zero;
            nextForceVector = Vector3.zero;
            nextRotationVector = transform.forward;
            //fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void CallPhysicsUpdate()
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

            controller.Move((nextHorizontalMovementVector + nextVerticalVelocityVector + nextForceVector) * Time.deltaTime);
            //if (needToSetHeight)
            //{
            //    needToSetHeight = false;
            //    this.enabled = false;
            //    transform.position = targetHeight;
            //    this.enabled = true;

            //}

            //Debug.Log((nextHorizontalMovementVector + nextVerticalVelocityVector).y);

            if (keepRotation)
            {
                Rotate(nextRotationVector);
            }
            else
            {
                if (nextHorizontalMovementVector != Vector3.zero)
                {
                    nextHorizontalMovementVector.y = 0;

                    Rotate(nextHorizontalMovementVector);
                }
                //Debug.Log(nextHorizontalMovementVector);
            }
            //if (nextHorizontalMovementVector == Vector3.zero)
            //{
            //    return;
            //}

            nextVerticalVelocityVector = 5 * Time.deltaTime * Physics.gravity;
            nextForceVector= Vector3.zero;

        }

        public void UpdateNextVerticalVelocityVector(Vector3 velocityVector)
        {
            nextVerticalVelocityVector = velocityVector;
        }

        public void UpdateNextHorizontalMovementVector(Vector3 movementVector)
        {
            nextHorizontalMovementVector = movementVector;
            if (movementVector != Vector3.zero)
            {
                nextRotationVector = movementVector;
                nextRotationVector.y = 0;
            }
        }

        public void UpdateNextForceVector(Vector3 forceVector)
        {
            nextForceVector = forceVector;
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

        public void SetKeepRotate(bool keep)
        {
            keepRotation = keep;
        }
    }
}
