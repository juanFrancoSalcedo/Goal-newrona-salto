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

        public bool IsAnyKeyPressed() => Input.anyKeyDown;

        public bool IsAnyKeyHeld() => Input.anyKey;

        public bool IsAnyKeyReleased() => !Input.anyKey;
    }
}
