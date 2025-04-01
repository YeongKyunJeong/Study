using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class PlayerInputReader : MonoBehaviour
    {
        private Player player;
        public event Action<Vector2> MoveEvent;
        public event Action JumpEvent;
        public event Action WalkToggleEvent;
        public event Action DashEvent;
        public event Action AttackEvent;

        //public PlayerInputReader(Player _player)
        //{
        //    player = _player;
        //}

        public void Initialize(Player _player)
        {
            player = _player;
        }

        //private void OnDestroy()
        //{
        //    plyaerInputActions.Disable();
        //}

        //private void Awake()
        //{
        //    player = GetComponent<Player>();
        //}

        public Vector2 MovementInput { get; private set; }


        public void OnMove(InputValue value)
        {
            //if (context.canceled)
            //{
            //    MovementInput = Vector2.zero;
            //}

            MovementInput = value.Get<Vector2>();

            MoveEvent?.Invoke(MovementInput);
        }

        public void OnJump()
        {
            JumpEvent?.Invoke();
        }

        public void OnZoom(InputValue zoomDelta)
        {
            return;
            // To Do : Use CameraZommer;
        }

        public Vector2 GetMovementInput()
        {
            return MovementInput;
        }

        public void OnWalkToggle()
        {
            WalkToggleEvent?.Invoke();
        }

        public void OnDash()
        {
            DashEvent?.Invoke();
        }

        public void OnAttack()
        {
            AttackEvent?.Invoke();
        }
    }
}
