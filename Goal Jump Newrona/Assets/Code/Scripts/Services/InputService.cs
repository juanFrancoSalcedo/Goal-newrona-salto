using B_Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services
{
    public class InputService : Singleton<InputService>
    {
        private InputSystem_Actions _inputActions;

        public InputSystem_Actions Actions => _inputActions;

        public bool AnyKeyPressed => Keyboard.current?.anyKey.wasPressedThisFrame ?? false;

        public bool AnyKeyHeld => Keyboard.current?.anyKey.isPressed ?? false;

        public bool AnyKeyReleased => Keyboard.current?.anyKey.wasReleasedThisFrame ?? false;

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
