using Services;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LastKeyTracker : MonoBehaviour
{
    [SerializeField] TMP_Text lastKeyText;


    private Vector2 navigationInput;

    void Update()
    {
        navigationInput = Vector2.zero;

        // 1. Intentar con Gamepad DPAD
        if (Gamepad.current != null)
        {
            var dpad = Gamepad.current.dpad;
            if (dpad.up.isPressed) navigationInput = Vector2.up;
            else if (dpad.down.isPressed) navigationInput = Vector2.down;
            else if (dpad.left.isPressed) navigationInput = Vector2.left;
            else if (dpad.right.isPressed) navigationInput = Vector2.right;
        }

        // 2. Si no, intentar con Keyboard
        if (navigationInput == Vector2.zero && Keyboard.current != null)
        {
            var keyboard = Keyboard.current;

            if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
                navigationInput = Vector2.up;
            else if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
                navigationInput = Vector2.down;
            else if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
                navigationInput = Vector2.left;
            else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
                navigationInput = Vector2.right;
        }

        // Usar navigationInput
        if (navigationInput != Vector2.zero)
        {
            HandleNavigation(navigationInput);
        }
    }

    void HandleNavigation(Vector2 direction)
    {
        switch (direction)
        {
            case var d when d == Vector2.up:
                lastKeyText.text = "Navegar ARRIBA";
                break;
            case var d when d == Vector2.down:
                lastKeyText.text = "Navegar ABAJO";
                break;
            case var d when d == Vector2.left:
                lastKeyText.text = "Navegar IZQUIERDA";
                break;
            case var d when d == Vector2.right:
                lastKeyText.text = "Navegar DERECHA";
                break;
        }
    }

    //void Update()
    //{
    //    lastKeyText.text = InputService.Instance.IsGamepadConnected.ToString()+" "+InputService.Instance.IsJoystickConnected.ToString()+
    //        ""+InputService.Instance.GetJoystickStickValue() +"" + InputService.Instance.IsJoystickStickMoved();
    //}
}