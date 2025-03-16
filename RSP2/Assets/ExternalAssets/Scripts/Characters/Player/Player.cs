using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine movementStateMachine;

        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Transform mainCameraTransform { get; private set; }



        public PlayerRuntimeData RuntimeData;

        private Vector3 movementVector;

        private void Awake()
        {
            RuntimeData = new PlayerRuntimeData();
            movementStateMachine = new PlayerMovementStateMachine(this);

            if (InputReader == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }

            InputReader.Initialize(this);
            InputReader.MovementEvent += ReadAndSaveMovementInput;

            if (Controller == null)
            {
                throw new NotImplementedException("Character Controller Not Assigned");
            }

            if (mainCameraTransform == null)
            {
                throw new NotImplementedException("Main Camera Transform Not Assigned");
            }
        }

        private void Start()
        {
        }

        private void Update()
        {
            movementStateMachine.DeliverUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.DeliverPhysicsUpdate();
        }


        private void ReadAndSaveMovementInput(Vector2 newMovementInput)
        {
            RuntimeData.MovementInput = newMovementInput;
        }

        public void MoveCall()
        {
            Controller.Move(CalculateMovementVector());
        }

        private Vector3 CalculateMovementVector()
        {
            Vector3 forward = mainCameraTransform.forward;
            Vector3 right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            movementVector = forward*RuntimeData.MovementInput.y + right*RuntimeData.MovementInput.x;
            movementVector *= RuntimeData.MovementSpeedModifier;

            return movementVector;
        }

        //public void SubscribeMovementEvent(Action<Vector2> action)
        //{
        //    InputReader.MovementEvent += action;
        //}

        //public void CancelMovementEvent(Action<Vector2> action)
        //{
        //    InputReader.MovementEvent -= action;
        //}
    }

}
