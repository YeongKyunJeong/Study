using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MoverForEnemy : MonoBehaviour
    {
        private Enemy enemy;
        private CharacterController controller;

        private Vector3 nextHorizontalMovementVector;
        private Vector3 nextForceVector;
        private Vector3 nextRotationVector;

        // Start is called before the first frame update
        public void Initialize(Enemy _enemy)
        {
            enemy = _enemy;
            controller = enemy.Controller;

            nextHorizontalMovementVector = Vector3.zero;
            nextForceVector = Vector3.zero;
            nextRotationVector = transform.forward;
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
            // TODO:: Add falling logic
            controller.Move((nextHorizontalMovementVector + nextForceVector) * Time.deltaTime);


            if (nextHorizontalMovementVector != Vector3.zero)
            {
                nextHorizontalMovementVector.y = 0;
                Rotate(nextHorizontalMovementVector);
            }
            nextForceVector = Vector3.zero;
        }

        public void UpdateNextHorizontalMovementVector(Vector3 velocityVector)
        {
            nextHorizontalMovementVector = velocityVector;
            if (nextHorizontalMovementVector != Vector3.zero)
            {
                nextHorizontalMovementVector.y = 0;
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
    }
}
