using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace RSP2
{
    public class PlayerInputReader : MonoBehaviour
    {
        private Player player;
        private PlayerInput playerInputComponent;

        #region Action Maps
        private InputActionMap playerInputActionMap;
        private InputActionMap uIInputActionMap;
        #endregion

        public Vector2 MovementInput { get; private set; }

        #region Action Events

        public event Action<Vector2> MoveEvent;
        public event Action JumpEvent;
        public event Action WalkToggleEvent;
        public event Action DashEvent;
        public event Action AttackEvent;
        public event Action AimEvent;
        public event Action QSkillEvent;
        public event Action ESkillEvent;
        #endregion

        #region UI Field
        public event Action InventoryEvent;
        #endregion

        public event Action InteractionEvent;

        //public PlayerInputReader(Player _player)
        //{
        //    player = _player;
        //}

        public void Initialize(Player _player)
        {
            player = _player;
            playerInputComponent = GetComponent<PlayerInput>();

            playerInputActionMap = playerInputComponent.actions.FindActionMap("Player");
            uIInputActionMap = playerInputComponent.actions.FindActionMap("UI");

            playerInputComponent.actions.FindActionMap("Global").Enable();
            EnablePlayerInput(true);


            var module = EventSystem.current.GetComponent<InputSystemUIInputModule>();
        }

        public void EnablePlayerInput(bool isOn)
        {
            if (isOn)
            {
                playerInputActionMap.Enable();
                uIInputActionMap.Disable();
                return;
            }

            playerInputActionMap.Disable();
            uIInputActionMap.Enable();
            return;
            //playerInputComponent.enabled = isOn;
        }

        private void OnMove(InputValue value)
        {
            //if (context.canceled)
            //{
            //    MovementInput = Vector2.zero;
            //}

            MovementInput = value.Get<Vector2>();

            MoveEvent?.Invoke(MovementInput);
        }

        private void OnJump()
        {
            JumpEvent?.Invoke();
        }

        private void OnZoom(InputValue zoomDelta)
        {
            return;
            // TODO :: Use CameraZommer;
        }

        private Vector2 GetMovementInput()
        {
            return MovementInput;
        }

        private void OnWalkToggle()
        {
            WalkToggleEvent?.Invoke();
        }

        private void OnDash()
        {
            DashEvent?.Invoke();
        }

        private void OnAttack()
        {
            AttackEvent?.Invoke();
        }

        private void OnAim()
        {
            Debug.Log("Aim");
            AimEvent?.Invoke();
        }

        private void OnSkillQ()
        {
            QSkillEvent?.Invoke();
        }

        private void OnSkillE()
        {
            Debug.Log("E");
            ESkillEvent?.Invoke();
        }

        #region UI
        private void OnInventory()
        {
            InventoryEvent?.Invoke();
        }
        #endregion

        private void OnInteraction()
        {
            InteractionEvent?.Invoke();
        }
    }
}
