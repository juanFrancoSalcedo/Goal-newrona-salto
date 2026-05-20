using B_Extensions;
using System;
using UnityEngine;

public class ButtonSubmitForm : BaseButtonAttendant, IFormSubmitable
{

    private void Start()
    {
        buttonComponent.onClick.AddListener(Submit);
    }

    private void Submit()
    {
        GameStateContext.ChangeState(GameEventType.FormSubmitted);
    }

    public void EnableSubmit(bool enable)
    {
        buttonComponent.interactable = enable;
    }
}
