using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerMover : MonoBehaviour
    {
        private Player player;
        private PlayerRuntimeData runtimeData;
        private PlayerInputReader inputReader;
        private CharacterController controller;

        private Vector2 movementInputVector;
        private Vector3 horizontalMovementVector;
        private Transform mainCameraTransform;

        public void Initialize()
        {
            player = GetComponent<Player>();
            runtimeData = player.RuntimeData;
            inputReader = GetComponent<PlayerInputReader>();
            inputReader.MoveEvent += OnMoveInput;
            controller = player.Controller;
            mainCameraTransform = Camera.main.transform;

        }


        public void CallFixedUpdate()
        {
            DoHorizontalMovement();
        }

        private void OnMoveInput(Vector2 movementInput)
        {
            movementInputVector = movementInput;
        }

        private void DoHorizontalMovement()
        {
            if(movementInputVector == Vector2.zero)
            {
                Debug.Log("Movement Input Not Detected");
                return;
            }
            Debug.Log($"{movementInputVector.x }, {movementInputVector.y}");
            horizontalMovementVector = CalculateMovementVector();
            runtimeData.HorizontalMovementVector = horizontalMovementVector;

            controller.Move(horizontalMovementVector* Time.fixedDeltaTime);

            Rotate(horizontalMovementVector);


        }

        private void Rotate(Vector3 targetDir)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
                Time.fixedDeltaTime * runtimeData.RotationSpeedModifier);
        }

        private Vector3 forward;
        private Vector3 right;
        private Vector3 CalculateMovementVector()
        {
            forward = mainCameraTransform.forward;
            right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return (forward * movementInputVector.y + right * movementInputVector.x)
                * runtimeData.MovementSpeedModifier;
        }

    }
}
