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
        ManagerAudio.Instance.PlayKick();
        ManagerAudio.Instance.PlayCrowds();
    }

    bool jump = false;
    private void Update()
    {
        if (GameStateContext.State != GameEventType.FormSubmitted)
            return;

        if (InputService.Instance.IsAnyKeyPressed())
            jump = true;

        if (jump && InputService.Instance.IsAnyKeyPressed())
        {
            jump = false;
            Click();
        }
    }
}
