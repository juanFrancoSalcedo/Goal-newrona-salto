using System;
using B_Extensions;
using Services;
using UnityEngine;

namespace Features
{
    public class ButtonSubmitForm : BaseButtonAttendant, IFormSubmitable
    {
        public Action OnPass { get; set; }

        private bool _submitEnabled;

        public void EnableSubmit(bool enable)
        {
            _submitEnabled = enable;
            buttonComponent.interactable = enable;
        }

        private void OnEnable()
        {
            buttonComponent.onClick.AddListener(OnSubmitClick);
        }

        private void OnDisable()
        {
            buttonComponent.onClick.RemoveListener(OnSubmitClick);
        }

        private void OnSubmitClick()
        {
            if (!_submitEnabled) 
                return;
            ManagerAudio.Instance.PlayWhistelStart();
            OnPass?.Invoke();
        }
    }
}