using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace RSP2
{
    public enum ActionMap
    {
        Field,
        UI,
        Interaction,
        Global
    }

    public class PlayerInputReader : MonoBehaviour
    {
        private Player player;
        private PlayerInput playerInputComponent;

        #region Action Maps
        private InputActionMap fieldActionMap;
        private InputActionMap uIActionMap;
        private InputActionMap InteractionActionMap;
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

            fieldActionMap = playerInputComponent.actions.FindActionMap("Field");
            uIActionMap = playerInputComponent.actions.FindActionMap("UI");
            InteractionActionMap = playerInputComponent.actions.FindActionMap("Interaction");

            playerInputComponent.actions.FindActionMap("Global").Enable();

            EnableFieldInput(true);


            //var module = EventSystem.current.GetComponent<InputSystemUIInputModule>();
        }

        public void EnableFieldInput(bool fieldOnly = true)
        {
            fieldActionMap.Enable();

            if (fieldOnly)
            {
                uIActionMap.Disable();
                InteractionActionMap.Disable();
            }
            return;
        }

        public void EnableUIInput(bool uIOnly = true)
        {
            uIActionMap.Enable();

            if (uIOnly)
            {
                fieldActionMap.Disable();
                InteractionActionMap.Disable();
            }
            return;
        }

        public void EnableInteractionInput(bool interactionOnly = true)
        {
            InteractionActionMap.Enable();

            if (interactionOnly)
            {
                fieldActionMap.Disable();
                uIActionMap.Disable();
            }
            return;
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
