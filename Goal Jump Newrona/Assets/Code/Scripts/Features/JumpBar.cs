using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Features
{
    public class JumpBar : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _maxJumpTime = 5f;

        private void OnEnable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpUpdate += UpdateFillAmount;
        }

        private void OnDisable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpUpdate -= UpdateFillAmount;
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
