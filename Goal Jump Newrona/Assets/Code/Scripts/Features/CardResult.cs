using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardResult : MonoBehaviour
{
    [SerializeField] private TMP_Text _attemptText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private float _scaleDuration = 0.4f;
    [SerializeField] private float _targetScale = 1.1f;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();

        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void Configure(int attempt, float time)
    {
        _attemptText.text = $"Intento {attempt}";
        _timeText.text = $"{time:F2}s";
    }

    public void AnimateShow()
    {
        _canvasGroup.alpha = 0f;
        _rectTransform.localScale = Vector3.one * 0.8f;

        _canvasGroup.DOFade(1f, _fadeDuration);
        _rectTransform.DOScale(Vector3.one, _scaleDuration).SetEase(Ease.OutBack);
    }

    public void AnimateHighlight()
    {
        _rectTransform.DOScale(Vector3.one * _targetScale, _scaleDuration / 2).SetEase(Ease.InOutSine);
        _rectTransform.DOScale(Vector3.one, _scaleDuration / 2).SetEase(Ease.InOutSine).SetDelay(_scaleDuration / 2);
    }
}
