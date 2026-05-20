using DG.Tweening;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Image imageBackground;
        [SerializeField] private Sprite before;
        [SerializeField] private Sprite after;
        //[SerializeField] private float _horizontalOffset = 80f;

        //private static bool _isLeft = true;

        private void OnEnable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpEnd += Initialize;
        }

        private void OnDisable()
        {
            if (JumpManager.Instance != null)
                JumpManager.Instance.OnTimeJumpEnd -= Initialize;
        }

        public void Initialize(float time)
        {
            _timeText.text = $"{time:F3} s";
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_timeText.DOFade(0, 0f));
            sequence.Append(_timeText.DOFade(1, 2f));
            imageBackground.sprite = after;
        }
    }
}
