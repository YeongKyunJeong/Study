using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        public PlayerInput Input { get; private set; }

        private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            MainCameraTransform = Camera.main.transform;
            Input = GetComponent<PlayerInput>();
            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingStates);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
