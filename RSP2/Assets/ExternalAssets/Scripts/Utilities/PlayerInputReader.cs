using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class PlayerInputReader : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        private Player player;
        private PlayerInputActions plyaerInputActions;
        public event Action<Vector2> MovementEvent;

        //public PlayerInputReader(Player _player)
        //{
        //    player = _player;
        //}

        public void Initialize(Player _player)
        {
            player = _player;
            plyaerInputActions = new PlayerInputActions();
            plyaerInputActions.Player.SetCallbacks(this);
            plyaerInputActions.Enable();
        }
        private void OnDestroy()
        {
            plyaerInputActions.Disable();
            plyaerInputActions.Player.RemoveCallbacks(this);
        }

        //private void OnEnable()
        //{
        //    plyaerInputActions.Enable();
        //}

        //private void OnDisable()
        //{
        //    plyaerInputActions.Disable();
        //}

        public Vector2 MovementInput { get; private set; }

        public void OnDash(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }


        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                MovementInput = Vector2.zero;
            }

            MovementInput = context.ReadValue<Vector2>();

            //Debug.Log($"{MovementInput.x}, {MovementInput.y}");

            MovementEvent?.Invoke(MovementInput);
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            return;
            // To Do : Use CameraZommer;
        }

        public Vector2 GetMovementInput()
        {
            return MovementInput;
        }
    }
}
