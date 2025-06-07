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

        public event Action<Vector2> MoveEvent;
        public event Action JumpEvent;
        public event Action WalkToggleEvent;
        public event Action DashEvent;
        public event Action AttackEvent;
        public event Action AimEvent;
        public event Action QSkillEvent;
        public event Action ESkillEvent;

        #region UI Field
        public event Action InventoryEvent;
        #endregion

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
            Debug.Log("Point: " + module.point.action?.activeControl?.path);
            Debug.Log("LeftClick: " + module.leftClick.action?.activeControl?.path);
        }

        //void Update()
        //{
        //    if (Mouse.current.leftButton.wasPressedThisFrame)
        //    {
        //        PointerEventData data = new PointerEventData(EventSystem.current);
        //        data.position = Mouse.current.position.ReadValue();

        //        List<RaycastResult> results = new List<RaycastResult>();
        //        EventSystem.current.RaycastAll(data, results);

        //        Debug.Log("Raycast 결과 수: " + results.Count);
        //        foreach (var result in results)
        //        {
        //            Debug.Log("감지된 오브젝트: " + result.gameObject.name);
        //        }
        //    }
        //}
        //private void OnDestroy()
        //{
        //    plyaerInputActions.Disable();
        //}

        //private void Awake()
        //{
        //    player = GetComponent<Player>();
        //}

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
            // TODO :: Use CameraZommer;
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

        public void OnAim()
        {
            Debug.Log("Aim");
            AimEvent?.Invoke();
        }

        public void OnSkillQ()
        {
            QSkillEvent?.Invoke();
        }

        public void OnSkillE()
        {
            Debug.Log("E");
            ESkillEvent?.Invoke();
        }

        #region UI
        public void OnInventory()
        {
            InventoryEvent?.Invoke();
        }
        #endregion
    }
}
