using Services;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features
{
    public class JumpBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _maxJumpTime = 5f;
        [SerializeField] CountdownTimer countTimer;

        private void OnEnable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpUpdate += UpdateFillAmount;
            countTimer.OnCountdownFinished += Reset;
        }

        private void Reset() => _fillImage.fillAmount = 0f;

        private void OnDisable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpUpdate -= UpdateFillAmount;
            countTimer.OnCountdownFinished += Reset;
        }

        private void UpdateFillAmount(float jumpTime)
        {
            if (_fillImage == null)
                return;
            //print($"var {(float)jumpTime / _maxJumpTime}");
            _fillImage.fillAmount = Mathf.Clamp01(jumpTime / _maxJumpTime);
        }
    }
}
