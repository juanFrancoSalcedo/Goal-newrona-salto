using B_Extensions;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Services
{
    public class InputService : Singleton<InputService>
    {
        private InputSystem_Actions _inputActions;

        public InputSystem_Actions Actions => _inputActions;

        public bool AnyKeyPressed => Keyboard.current?.anyKey.wasPressedThisFrame ?? false;

        public bool AnyKeyHeld => Keyboard.current?.anyKey.isPressed ?? false;

        public bool AnyKeyReleased => Keyboard.current?.anyKey.wasReleasedThisFrame ?? false;

        public bool GetAnyGamepadButtonPressed() 
        {
            bool pressed = false;
            if (Gamepad.current != null)
            { 
                if (Gamepad.current.allControls.Count(c => c.IsPressed()) > 0)
                {
                    pressed = true;
                }
            }
            return pressed;
        }
        

        public bool AnyGamepadButtonHeld =>
            Gamepad.current?.allControls.Any(b => b.IsPressed()) ?? false;

        public bool GetAnyGamepadButtonReleased()
        {
            bool released = false;
            if (Gamepad.current != null)
            {
                if (Gamepad.current.allControls.Any(c => c is ButtonControl bc && bc.wasReleasedThisFrame))
                {
                    Debug.Log("�Alg�n bot�n de gamepad fue liberado!");
                    released = true;
                }

                if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
                {
                    Debug.Log("Bot�n A (South) liberado");
                }
            }
            return released;
        }
            

        public bool IsGamepadConnected => Gamepad.current != null;

        public bool IsJoystickConnected => Joystick.current != null;

        public Vector2 GetJoystickStickValue() =>
            Joystick.current?.stick.ReadValue() ?? Vector2.zero;

        public bool IsJoystickStickMoved() =>
            Joystick.current?.stick.IsActuated() ?? false;

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}
