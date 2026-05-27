using B_Extensions;
using UnityEngine;

namespace Services
{
    public class InputService : Singleton<InputService>
    {
        public float GetAxisHorizontal() => Input.GetAxis("Horizontal");

        public float GetAxisVertical() => Input.GetAxis("Vertical");

        public bool IsAnyAxisMoved(float deadZone = 0.1f) =>
            Mathf.Abs(Input.GetAxis("Horizontal")) > deadZone ||
            Mathf.Abs(Input.GetAxis("Vertical")) > deadZone;

        public bool IsAnyKeyPressed() => Input.anyKeyDown && !AnyMouseButtonDown();

        public bool IsAnyKeyHeld() => Input.anyKey && !AnyMouseButton();

        public bool IsAnyKeyReleased() => !IsAnyKeyHeld();

        private bool AnyMouseButtonDown() =>
            Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);

        private bool AnyMouseButton() =>
            Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
    }
}
