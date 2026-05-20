using B_Extensions;
using Services;
using System;
using UnityEngine;

public class ButtonStartGame : BaseButtonAttendant
{
    void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);        
    }

    private void StartGame()
    {
        if (GameStateContext.State != GameEventType.FormSubmitted)
            return;
        GameStateContext.ChangeState(GameEventType.IntroCountDown);
    }

    bool jump = false;
    private void Update()
    {
        if (GameStateContext.State != GameEventType.FormSubmitted)
            return;

        if (InputService.Instance.AnyKeyReleased)
        {
            jump = true;
            
        }

        if (jump && InputService.Instance.AnyKeyPressed)
        {
            jump = false;
            Click();
        }
    }

}
