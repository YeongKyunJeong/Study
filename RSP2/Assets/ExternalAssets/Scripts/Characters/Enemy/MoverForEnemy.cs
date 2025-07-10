using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MoverForEnemy : MonoBehaviour
    {
        private Enemy enemy;
        private RuntimeDataForEnemy runtimeData;
        private CharacterController controller;

        private Vector3 nextVerticalVelocityVector;
        private Vector3 nextHorizontalMovementVector;

        // No falling state for enemy 
        private Vector3 fallingCalculatedVelocity;

        private Vector3 nextForceVector;
        //private Vector3 nextRotationVector;

        private bool onlyRotateThisFrame;

        // Start is called before the first frame update
        public void Initialize(Enemy _enemy)
        {
            enemy = _enemy;
            runtimeData = _enemy.RuntimeData;
            controller = _enemy.Controller;

            nextHorizontalMovementVector = Vector3.zero;
            nextVerticalVelocityVector = 5 * Time.deltaTime * Physics.gravity;
            nextForceVector = Vector3.zero;
            //nextRotationVector = transform.forward;

            onlyRotateThisFrame = false;
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

            if (nextHorizontalMovementVector != Vector3.zero)
            {
                Rotate(nextHorizontalMovementVector);
            }

            if (onlyRotateThisFrame)
            {
                onlyRotateThisFrame = false;
                nextHorizontalMovementVector = Vector3.zero;
            }

            controller.Move((nextVerticalVelocityVector + nextHorizontalMovementVector + nextForceVector) * Time.deltaTime);

            ApplyGravity();

            nextForceVector = Vector3.zero;
        }

        private void ApplyGravity()
        {
            if (controller.isGrounded) // Reset falling velocity
            {
                nextVerticalVelocityVector = 2 * Time.deltaTime * Physics.gravity;
                fallingCalculatedVelocity = Vector3.zero;
            }
            else
            {
                fallingCalculatedVelocity += Time.deltaTime * Physics.gravity;
                nextVerticalVelocityVector = fallingCalculatedVelocity;
            }
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
                nextHorizontalMovementVector.y = 0; // To safety

                //nextRotationVector = movementVector;
                //nextRotationVector.y = 0;
            }
        }

        public void UpdateNextForceVector(Vector3 forceVector)
        {
            nextForceVector = forceVector;
        }

        private void Rotate(Vector3 targetDir)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
                Time.deltaTime * enemy.RotationSpeedModifier);
        }

        public void SetOnlyRotateThisFrame(bool isKeep)
        {
            onlyRotateThisFrame = isKeep;
        }
    }
}
