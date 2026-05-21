using Services;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastKeyTracker : MonoBehaviour
{
    [SerializeField] TMP_Text lastKeyText;

    private Vector2 navigationInput;

    void Update()
    {
        lastKeyText.text = "Última acción: " + InputService.Instance.IsAnyAxisMoved().ToString();
    }

    //void Update()
    //{
    //    lastKeyText.text = InputService.Instance.IsGamepadConnected.ToString()+" "+InputService.Instance.IsJoystickConnected.ToString()+
    //        ""+InputService.Instance.GetJoystickStickValue() +"" + InputService.Instance.IsJoystickStickMoved();
    //}
}