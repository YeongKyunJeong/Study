using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : MonoBehaviour
    {

        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public PlayerMover Mover { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Transform MainCameraTransform { get; private set; }
        [field: SerializeField] public PlayerScriptableObject SOData { get; private set; }

        public MovementStateMachineForPlayer MovementStateMachine { get; private set; }
        public PlayerRuntimeData RuntimeData { get; private set; }

        private void Awake()
        {
            RuntimeData = new PlayerRuntimeData();
            MovementStateMachine = new MovementStateMachineForPlayer(this);


            if (SOData == null)
            {
                throw new NotImplementedException("Player Scriptable Object Not Assigned");
            }

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

            if (MainCameraTransform == null)
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
            MovementStateMachine.CallUpdate();
        }

        private void FixedUpdate()
        {
            MovementStateMachine.CallPhysicsUpdate();
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
