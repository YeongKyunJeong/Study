using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class EnemyMover : MonoBehaviour
    {
        private Enemy enemy;
        private CharacterController controller;

        private Vector3 nextHorizontalMovementVector;
        private Vector3 nextRotationVector;

        // Start is called before the first frame update
        public void Initialize(Enemy _enemy)
        {
            enemy = _enemy;
            controller = enemy.Controller;

            nextHorizontalMovementVector = Vector3.zero;
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
            controller.Move(nextHorizontalMovementVector * Time.deltaTime);


            if (nextHorizontalMovementVector != Vector3.zero)
            {
                nextHorizontalMovementVector.y = 0;
                Rotate(nextHorizontalMovementVector);
            }
        }

        public void UpdateNextHorizontalMovementVector(Vector3 velocityVector)
        {
            nextHorizontalMovementVector = velocityVector;
            if (nextHorizontalMovementVector != Vector3.zero)
            {
                nextHorizontalMovementVector.y = 0;
            }
        }

        private void Rotate(Vector3 targetDir)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
                Time.deltaTime * enemy.RotationSpeedModifier);
        }
    }
}
