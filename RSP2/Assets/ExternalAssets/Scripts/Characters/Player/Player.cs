using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : MonoBehaviour
    {
        private MovementStateMachineForPlayer movementStateMachine;

        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public PlayerMover Mover { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Transform mainCameraTransform { get; private set; }

        public PlayerRuntimeData RuntimeData { get; private set; }

        private void Awake()
        {
            RuntimeData = new PlayerRuntimeData();
            movementStateMachine = new MovementStateMachineForPlayer(this);

            if (InputReader == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }
            InputReader.Initialize(this);

            if (Mover == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }
            Mover.Initialize();

            if (Controller == null)
            {
                throw new NotImplementedException("Character Controller Not Assigned");
            }

            if (mainCameraTransform == null)
            {
                throw new NotImplementedException("Main Camera Transform Not Assigned");
            }

            //if (Rigidbody == null)
            //{
            //    throw new NotImplementedException("Rigidbody Not Assigned");
            //}
        }

        private void Start()
        {
        }

        private void Update()
        {
            movementStateMachine.CallUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.CallPhysicsUpdate();
            Mover.CallFixedUpdate();
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
